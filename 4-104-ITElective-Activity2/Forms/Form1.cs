using _4_104_ITElective_Activity2.core;
using _4_104_ITElective_Activity2.Core;
using _4_104_ITElective_Activity2.Core.DI;
using _4_104_ITElective_Activity2.forms;
using _4_104_ITElective_Activity2.Modules.User;
using System.Windows.Forms;

namespace _4_104_ITElective_Activity2.Forms
{
    public class LogoutSignal
    {
        public required bool signal;
    }
    public partial class Form1 : Form
    {
        private readonly UserRepository _userRepository = ServiceLocator.Get<UserRepository>();

        public Form1()
        {
            InitializeComponent();
            EventBus.Subscribe<LogoutSignal>(OnLogout);
        }
        private void OnLogout(LogoutSignal data)
        {
            if (data.signal == true)
            {
                AppStore.Clear<UserSession>();
                this.Show();
                usernameTextBox.Clear();
                passwordTextbox.Clear();
            }
        }
        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextbox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User? user = _userRepository.FindByCredentials(username, password);

            if (user == null)
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AppStore.Set(new UserSession(user));

            this.Hide();

            Form destination = user.Role == "admin"
                ? new Admin()
                : new MainForm();

            destination.Show();
        }
    }
}
