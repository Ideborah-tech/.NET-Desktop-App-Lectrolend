using LectroLend.Core.Data;
using LectroLend.Desktop.Controllers;
using LectroLend.Desktop.Views;

namespace LectroLend.Desktop;

// The app starts here.
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        // ---- Step 1: connect to the database ----
        // We try to connect. If it fails (for example a wrong password),
        // we show the connection window and try again.
        DbSettings settings = DbSettings.Load();
        while (true)
        {
            string problem;
            try
            {
                DatabaseInitializer.Initialize(settings);
                break; // it worked, leave the loop
            }
            catch (Exception ex)
            {
                problem = ex.Message;
            }

            ConnectionForm connectionForm = new ConnectionForm(settings, problem);
            if (connectionForm.ShowDialog() != DialogResult.OK)
                return; // the user closed the window: stop the app

            settings = connectionForm.Settings;
        }

        // ---- Step 2: create the controllers (MVC) ----
        AppControllers app = new AppControllers(settings);

        // ---- Step 3: sign in, then show the main window ----
        // After "Sign out" we go back to the sign-in window.
        while (true)
        {
            LoginForm loginForm = new LoginForm(app);
            if (loginForm.ShowDialog() != DialogResult.OK)
                return; // the user closed the sign-in window: stop the app

            MainForm mainForm = new MainForm(app);
            Application.Run(mainForm); // waits here until the main window is closed

            if (!mainForm.SignedOut)
                return; // closed with the X button: stop the app
        }
    }
}
