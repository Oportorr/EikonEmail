using System;
using System.Windows.Forms;
using System.Xml;
using System.Configuration;
using System.Net.Mail;
using System.Net.Configuration;
using System.Reflection;
namespace EIKON
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //=========================================================================//

        //=========================================================================//
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            var EikonEmaiil = new SendMail();
           string lcReturn= EikonEmaiil.SendEikonEmail("oscarportorreal@gmail.com", "Volante EIKON", "Este es el Cuerpo del Correo", "c:\\test\\interface.txt", "Este es el Asunto 6");
           this.textBox1.Text = lcReturn;
               

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //config.AppSettings.Settings["host"].Value = "coreo.elcorreo.vacano";
            //config.Save(ConfigurationSaveMode.Modified);

            //ConfigurationManager.RefreshSection("appSettings");

           string fileName = "C:\\Users\\oscar.portorreal\\Documents\\visual studio 2013\\Projects\\EikonEmail\\EikonEmail\\bin\\Debug\\EikonEmail.exe.config";
            UpdateConfig("host","probando.com",fileName);
        //    var configFile = ConfigurationManager.OpenExeConfiguration(fileName);

        //    configFile.AppSettings.Settings["host"].Value = "correo.vacano.com";


        //    configFile.Save();


           //UpdateAppSettings("host", "correo.vacano.com");
        }





        private void UpdateAppSettings(string theKey, string theValue)
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("mailSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes[0].Value.Equals("host"))
                        {
                            node.Attributes[1].Value = "New Value";
                        }
                    }
                }
            }

            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            ConfigurationManager.RefreshSection("appSettings");
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = "EikonEmail.vshost.exe.config";




            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (ConfigurationManager.AppSettings.AllKeys.ToString().Contains(theKey))
            {
                configuration.AppSettings.Settings[theKey].Value = theValue;
            }

            configuration.AppSettings.Settings["host"].Value = "2.0.0";
            configuration.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }


        //Where: fileName is the full path + application name (c:\project\application.exe)
        //In your case, change the AppSetting by Sections
        private void UpdateConfig(string key, string value, string fileName)
        {


             Configuration myconfig;
             MailSettingsSectionGroup mymailSettings;
            int port3;
            myconfig = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            mymailSettings = (MailSettingsSectionGroup)myconfig.GetSectionGroup("system.net/mailSettings");

            try{
                DialogResult result = MessageBox.Show("Are you sure you want to Save?",
            "Are you sure you want to Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
 
        if (result == DialogResult.Yes)
        {
            int.TryParse(PortTextBox.Text, out port3);
            mymailSettings.Smtp.From = "Testing";//FromTextBox.Text;
            mymailSettings.Smtp.Network.Host = HostTextBox.Text;
            mymailSettings.Smtp.Network.Port = port3;
            mymailSettings.Smtp.Network.UserName = UserNameTextBox.Text;
            mymailSettings.Smtp.Network.Password = PasswordTextBox.Text;
            mymailSettings.Smtp.Network.DefaultCredentials = DefaultCredentialsCheckBox.Checked;

            myconfig.Save(ConfigurationSaveMode.Modified);
 
            MessageBox.Show("The Operation is successfully completed", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, ex.GetType().ToString(),
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }


        //var configFile = ConfigurationManager.OpenExeConfiguration(fileName);
        //configFile.AppSettings.Settings[key].Value = value;

        //configFile.Save();

            string defaultCredentials = "";
            //You can access the network credentials in the following way.
            //Read the SmtpClient section from the config file    
            Configuration Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var mailSettings = Config.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
            if (mailSettings != null)
            {
                int port2 = mailSettings.Smtp.Network.Port;
                string from = mailSettings.Smtp.From;
                string host = mailSettings.Smtp.Network.Host;
                string pwd = mailSettings.Smtp.Network.Password;
                string uid = mailSettings.Smtp.Network.UserName;
                

            }

            var smtp = new System.Net.Mail.SmtpClient();
            //Cast the newtwork credentials in to the NetworkCredential class and use it .
            var credential = (System.Net.NetworkCredential)smtp.Credentials;
            string strHost = smtp.Host;
            int port = smtp.Port;
            string strUserName = credential.UserName;
            string strFromPass = credential.Password;

            if (smtp.UseDefaultCredentials != null )
            {
                 defaultCredentials = smtp.UseDefaultCredentials.ToString();
            } else  defaultCredentials="";

            credential.UserName = "probando";
            credential.Password = "Este es elpass";
            smtp.Port = 99;
            smtp.Host = "oscarsoft.net";
            Config.Save(ConfigurationSaveMode.Modified,true);
            Config.Save();
       //     string defaultCre = credential.defaultCredentials;      



     }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }



    }
}
