using _4_104_ITElective_Activity2.Core.DI;
using _4_104_ITElective_Activity2.Modules.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace _4_104_ITElective_Activity2.Forms
{
    public partial class UserForm : Form
    {
        private static readonly Color ColorOk      = Color.FromArgb(198, 239, 206);
        private static readonly Color ColorError   = Color.FromArgb(255, 199, 206);
        private static readonly Color ColorDefault = Color.White;

        private readonly UserService _userService;
        private CancellationTokenSource _usernameCts = new();
        private bool _usernameAvailable = false;

        public FormMode Mode { get; }
        public User? Target { get; }

        // Holds the pending image selection until the form is saved
        public Stream? PendingImageStream       { get; private set; }
        public string? PendingImageFileName     { get; private set; }
        public string? PendingImageContentType  { get; private set; }

        public UserForm(FormMode mode, User? target = null)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            _userService = ServiceLocator.Get<UserService>();
            Mode   = mode;
            Target = target;
            formTitle.Text = mode == FormMode.Add ? "Create User" : "Edit User";
            actionBtn.Text      = mode == FormMode.Add ? "Create" : "Save";
            actionBtn.Enabled   = false;
            actionBtn.BackColor = Color.FromArgb(80, 80, 80);
            actionBtn.ForeColor = Color.FromArgb(160, 160, 160);

            formUsernameTxtBox.TextChanged        += (s, e) => CheckUsernameAsync();
            formNewPasswordTxtBox.TextChanged     += (s, e) => { CheckPasswordMatch(); ValidateForm(); };
            formConfirmPasswordTxtBox.TextChanged += (s, e) => { CheckPasswordMatch(); ValidateForm(); };
            formRoleComboBox.SelectedIndexChanged += (s, e) => ValidateForm();

            FillTextBoxes();

            // In edit mode the pre-filled username is already valid
            if (Mode == FormMode.Edit && Target != null)
            {
                _usernameAvailable = true;
                if (usernamePanel != null) usernamePanel.BackColor = ColorOk;
                ValidateForm();
            }
        }

        private async void CheckUsernameAsync()
        {
            // Cancel any previous in-flight check
            _usernameCts.Cancel();
            _usernameCts = new CancellationTokenSource();
            var token = _usernameCts.Token;

            string username = formUsernameTxtBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                if (usernamePanel != null) usernamePanel.BackColor = ColorDefault;
                _usernameAvailable = false;
                ValidateForm();
                return;
            }

            // Small delay so we don't hit DB on every single keystroke
            try { await Task.Delay(400, token); }
            catch (TaskCanceledException) { return; }

            int? excludeId = Mode == FormMode.Edit ? Target?.Id : null;
            bool exists    = await _userService.UsernameExistsAsync(username, excludeId);

            if (token.IsCancellationRequested) return;

            _usernameAvailable = !exists;
            if (usernamePanel != null)
                usernamePanel.BackColor = exists ? ColorError : ColorOk;
            ValidateForm();
        }

        private void CheckPasswordMatch()
        {
            string pw      = formNewPasswordTxtBox.Text;
            string confirm = formConfirmPasswordTxtBox.Text;

            if (string.IsNullOrEmpty(pw) && string.IsNullOrEmpty(confirm))
            {
                passwordPanel.BackColor        = ColorDefault;
                confirmPasswordPanel.BackColor = ColorDefault;
                return;
            }

            bool match = pw == confirm;
            passwordPanel.BackColor        = match ? ColorOk : ColorError;
            confirmPasswordPanel.BackColor = match ? ColorOk : ColorError;
        }

        private void ValidateForm()
        {
            bool roleOk = formRoleComboBox.SelectedIndex >= 0;

            bool passwordOk = Mode == FormMode.Add
                ? !string.IsNullOrWhiteSpace(formNewPasswordTxtBox.Text) &&
                  formNewPasswordTxtBox.Text == formConfirmPasswordTxtBox.Text
                : true;

            bool valid = _usernameAvailable && roleOk && passwordOk;

            actionBtn.Enabled   = valid;
            actionBtn.BackColor = valid ? Color.FromArgb(34, 139, 34) : Color.FromArgb(80, 80, 80);
            actionBtn.ForeColor = valid ? Color.White                  : Color.FromArgb(160, 160, 160);
        }
        private async void actionBtn_Click(object sender, EventArgs e)
        {
            actionBtn.Enabled = false;
            actionBtn.Text    = "Saving...";

            try
            {
                if (Mode == FormMode.Add)
                {
                    await _userService.CreateUserAsync(new CreateUserDTO
                    {
                        Username         = formUsernameTxtBox.Text.Trim(),
                        Password         = formNewPasswordTxtBox.Text,
                        Role             = formRoleComboBox.SelectedItem!.ToString()!,
                        FirstName        = NullIfEmpty(formFirstnameTxtBox.Text),
                        MiddleName       = NullIfEmpty(formMiddlenameTxtBox.Text),
                        LastName         = NullIfEmpty(formLastnameTxtBox.Text),
                        Contact          = NullIfEmpty(formContactTxtBox.Text),
                        PermanentAddress = NullIfEmpty(formPermanentAddressTxtBox.Text),
                        CurrentAddress   = NullIfEmpty(formCurrentAddressTxtBox.Text),
                        Gender           = GetGenderFromForm(),
                        ImageStream      = PendingImageStream,
                        ImageFileName    = PendingImageFileName,
                        ImageContentType = PendingImageContentType,
                    });
                }
                else
                {
                    await _userService.UpdateUserAsync(new UpdateUserDTO
                    {
                        Id               = Target!.Id,
                        Username         = formUsernameTxtBox.Text.Trim(),
                        Role             = formRoleComboBox.SelectedItem!.ToString()!,
                        FirstName        = NullIfEmpty(formFirstnameTxtBox.Text),
                        MiddleName       = NullIfEmpty(formMiddlenameTxtBox.Text),
                        LastName         = NullIfEmpty(formLastnameTxtBox.Text),
                        Contact          = NullIfEmpty(formContactTxtBox.Text),
                        PermanentAddress = NullIfEmpty(formPermanentAddressTxtBox.Text),
                        CurrentAddress   = NullIfEmpty(formCurrentAddressTxtBox.Text),
                        Gender           = GetGenderFromForm(),
                        NewPassword      = NullIfEmpty(formNewPasswordTxtBox.Text),
                        ExistingImagePath = Target.ImagePath,
                        ImageStream      = PendingImageStream,
                        ImageFileName    = PendingImageFileName,
                        ImageContentType = PendingImageContentType,
                    });
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save user: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                actionBtn.Enabled = true;
                actionBtn.Text    = Mode == FormMode.Add ? "Create" : "Save";
            }
        }

        private string? GetGenderFromForm()
        {
            if (radioButton1.Checked) return "Male";
            if (radioButton2.Checked) return "Female";
            if (radioButton3.Checked) return "Other";
            return null;
        }

        private static string? NullIfEmpty(string value)
            => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

        private void Chance_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title  = "Select Profile Image",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            // Dispose previous pending stream if user changed selection again
            PendingImageStream?.Dispose();

            PendingImageStream      = File.OpenRead(dialog.FileName);
            PendingImageFileName    = Path.GetFileName(dialog.FileName);
            PendingImageContentType = dialog.FileName.EndsWith(".png") ? "image/png" : "image/jpeg";

            pictureBox1.Image = Image.FromFile(dialog.FileName);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _usernameCts.Cancel();
            _usernameCts.Dispose();
            PendingImageStream?.Dispose();
            base.OnFormClosed(e);
        }

        private async void FillTextBoxes()
        {
            if (Target == null) return;

            formUsernameTxtBox.Text           = Target.Username;
            formRoleComboBox.SelectedItem     = Target.Role;
            formFirstnameTxtBox.Text          = Target.FirstName        ?? string.Empty;
            formMiddlenameTxtBox.Text         = Target.MiddleName       ?? string.Empty;
            formLastnameTxtBox.Text           = Target.LastName         ?? string.Empty;
            formContactTxtBox.Text            = Target.Contact          ?? string.Empty;
            formPermanentAddressTxtBox.Text   = Target.PermanentAddress ?? string.Empty;
            formCurrentAddressTxtBox.Text     = Target.CurrentAddress   ?? string.Empty;

            switch (Target.Gender?.ToLower())
            {
                case "male":   radioButton1.Checked = true; break;
                case "female": radioButton2.Checked = true; break;
                case "other":  radioButton3.Checked = true; break;
            }

            string? imageUrl = await _userService.GetProfileImageUrlAsync(Target.ImagePath);
            if (imageUrl != null)
                pictureBox1.Load(imageUrl);
        }

    }
}
