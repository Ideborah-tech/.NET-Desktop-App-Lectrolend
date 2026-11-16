using System.Text.Json;
using Npgsql;

namespace LectroLend.Core.Data;

// The details needed to connect to PostgreSQL.
// They are saved in a small file so the app remembers them next time:
//     C:\Users\<you>\AppData\Roaming\LectroLend\connection.json
public class DbSettings
{
    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 5432;
    public string Database { get; set; } = "lectrolend";
    public string Username { get; set; } = "postgres";
    public string Password { get; set; } = "";

    // Where the settings file is saved.
    public static string FilePath()
    {
        string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        return Path.Combine(appData, "LectroLend", "connection.json");
    }

    // Reads the settings file. If there is no file yet, we use the default values above.
    public static DbSettings Load()
    {
        try
        {
            if (File.Exists(FilePath()))
            {
                string json = File.ReadAllText(FilePath());
                return JsonSerializer.Deserialize<DbSettings>(json);
            }
        }
        catch (Exception)
        {
            // The file is broken: just use the default values.
        }
        return new DbSettings();
    }

    // Writes the settings to the file.
    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath()));
        string json = JsonSerializer.Serialize(this);
        File.WriteAllText(FilePath(), json);
    }

    // Builds the connection text that PostgreSQL understands.
    // Pass a database name to connect to a different database (used once to create ours).
    public string ConnectionString(string databaseName = null)
    {
        if (databaseName == null)
            databaseName = Database;

        NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder();
        builder.Host = Host;
        builder.Port = Port;
        builder.Database = databaseName;
        builder.Username = Username;
        builder.Password = Password;
        builder.Timeout = 5; // give up after 5 seconds if the server does not answer
        return builder.ConnectionString;
    }
}
