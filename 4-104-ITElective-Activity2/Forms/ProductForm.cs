using _4_104_ITElective_Activity2.Core.DI;
using _4_104_ITElective_Activity2.modules.item;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _4_104_ITElective_Activity2.Forms
{
    public partial class ProductForm : Form
    {
        private static readonly Color ColorOk = Color.FromArgb(198, 239, 206);
        private static readonly Color ColorError = Color.FromArgb(255, 199, 206);
        private static readonly Color ColorDefault = Color.White;

        private readonly ProductService _productService;

        public FormMode Mode { get; }
        public Product? Target { get; }


        // Holds the pending image selection until the form is saved
        public Stream? PendingImageStream { get; private set; }
        public string? PendingImageFileName { get; private set; }
        public string? PendingImageContentType { get; private set; }

        public ProductForm(FormMode mode, Product? target = null)
        {
            InitializeComponent();
            _productService = ServiceLocator.Get<ProductService>();
            Mode = mode;
            Target = target;
            formTitle.Text = mode == FormMode.Add ? "Create Product" : "Edit Product";
            actionBtn.Text = mode == FormMode.Add ? "Create" : "Save";
            actionBtn.Enabled = false;

            actionBtn.BackColor = Color.FromArgb(80, 80, 80);
            actionBtn.ForeColor = Color.FromArgb(160, 160, 160);

            FillTextBoxes();

            formProductnameTxtBox.TextChanged += (s, e) => ValidateForm();
            formPriceTxtBox.TextChanged += (s, e) => ValidateForm();

            ValidateForm();
        }

        private void ValidateForm()
        {
            bool valid = !string.IsNullOrWhiteSpace(formProductnameTxtBox.Text)
                      && !string.IsNullOrWhiteSpace(formPriceTxtBox.Text);

            actionBtn.Enabled = valid;
            if (valid)
            {
                actionBtn.BackColor = Color.FromArgb(46, 139, 87);
                actionBtn.ForeColor = Color.White;
            }
            else
            {
                actionBtn.BackColor = Color.FromArgb(80, 80, 80);
                actionBtn.ForeColor = Color.FromArgb(160, 160, 160);
            }
        }

        private void Change_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Select Profile Image",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            // Dispose previous pending stream if user changed selection again
            PendingImageStream?.Dispose();
            PendingImageStream = File.OpenRead(dialog.FileName);
            PendingImageFileName = Path.GetFileName(dialog.FileName);
            PendingImageContentType = dialog.FileName.EndsWith(".png") ? "image/png" : "image/jpeg";

            pictureBox1.Image = Image.FromFile(dialog.FileName);
        }

        private async void actnBtn_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(formPriceTxtBox.Text, out double price))
            {
                MessageBox.Show("Price must be a valid number.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            actionBtn.Enabled = false;
            actionBtn.Text    = "Saving...";

            try
            {
                if (Mode == FormMode.Add)
                {
                    await _productService.AddProductAsync(new AddItemDTO
                    {
                        Name             = formProductnameTxtBox.Text.Trim(),
                        Price            = price,
                        IsAvailable      = checkBox1.Checked,
                        ImageStream      = PendingImageStream,
                        ImageFileName    = PendingImageFileName,
                        ImageContentType = PendingImageContentType,
                    });
                }
                else
                {
                    await _productService.UpdateProductAsync(new UpdateItemDTO
                    {
                        Id               = Target!.id!.Value,
                        Name             = formProductnameTxtBox.Text.Trim(),
                        Price            = price,
                        IsAvailable      = checkBox1.Checked,
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
                MessageBox.Show($"Failed to save product: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                actionBtn.Enabled = true;
                actionBtn.Text    = Mode == FormMode.Add ? "Create" : "Save";
            }
        }

        private void FillTextBoxes()         {
            if (Target == null) return;

            formProductnameTxtBox.Text = Target.name;
            formPriceTxtBox.Text = Target.price.ToString();
            checkBox1.Checked = Target.isAvailable;
        }
    }
}
