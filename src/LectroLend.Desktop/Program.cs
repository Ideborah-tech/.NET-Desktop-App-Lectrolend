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

        // ---- Step 3: welcome page, sign in, then the main window ----
        // "Get started" on the welcome page opens the sign-in page.
        // "Back" on the sign-in page shows the welcome page again.
        // After "Sign out" we go back to the sign-in page.
        bool showWelcome = true;
        while (true)
        {
            if (showWelcome)
            {
                WelcomeForm welcomeForm = new WelcomeForm();
                if (welcomeForm.ShowDialog() != DialogResult.OK)
                    return; // the user closed the welcome page: stop the app
            }

            LoginForm loginForm = new LoginForm(app);
            DialogResult result = loginForm.ShowDialog();
            if (result == DialogResult.Retry)
            {
                showWelcome = true; // "Back" was clicked
                continue;
            }
            if (result != DialogResult.OK)
                return; // the user closed the sign-in window: stop the app

            showWelcome = false;
            MainForm mainForm = new MainForm(app);
            Application.Run(mainForm); // waits here until the main window is closed

            if (!mainForm.SignedOut)
                return; // closed with the X button: stop the app
        }
    }
}
