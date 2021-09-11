using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;

namespace CSDL3
{
    public partial class loginout : Form
    {
        public loginout()
        {
            InitializeComponent();
        }
        IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "gRbOC28Gr9uZuEhLV8UAxpEldozFpb5bYvYdDgdi",
            BasePath = "https://winform-esp-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;

        private void loginout_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(ifc);
            }
            catch
            {
                MessageBox.Show("No Internet or Connection Problem");
            }
        }
        private void chekbxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chekbxShowPass.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            #region Condition
            if (string.IsNullOrWhiteSpace(txtUsername.Text) &&
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please Fill All The Fields");
                return;
            }
            #endregion
            FirebaseResponse res = client.Get(@"Users/" + txtUsername.Text);
            MyUser ResUser = res.ResultAs<MyUser>();
            MyUser CurUser = new MyUser()
            {
                Username = txtUsername.Text,
                Password = txtPassword.Text
            };
            if (MyUser.IsEqual(ResUser, CurUser))
            {
                Form1 real = new Form1();
                real.Show();
                this.Hide();
                MessageBox.Show("Login successfully!", "Notify!");
            }
            else
            {
                MyUser.ShowErorr();
            }
        }
    }
}
