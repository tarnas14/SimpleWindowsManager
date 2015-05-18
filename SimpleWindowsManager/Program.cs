namespace SimpleWindowsManager
{
    using System;
    using System.Windows.Forms;

    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var program = new Program();
            program.Run();
        }

        public void Run()
        {
            using (var mainForm = new Switcher())
            {
                Application.Run(mainForm);
            }
        }
    }
}
