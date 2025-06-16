using System;
using System.IO;
using System.Windows.Forms;

namespace Erp_V1
{
    /// <summary>
    /// Displays the security log (login/history events) from SecurityLogs.txt
    /// under %PROGRAMDATA%\Erp_V1, showing Timestamp, UserNo, FullName, MAC, and Details.
    /// Allows clearing the entire history as well.
    /// </summary>
    public partial class FrmLoginHistory : Form
    {
        /// <summary>
        /// Full path to SecurityLogs.txt in CommonApplicationData\Erp_V1.
        /// </summary>
        private readonly string _logFilePath;

        /// <summary>
        /// Initializes a new instance of <see cref="FrmLoginHistory"/> and computes the log file path.
        /// </summary>
        public FrmLoginHistory()
        {
            InitializeComponent();

            string commonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            _logFilePath = Path.Combine(commonAppData, "Erp_V1", "SecurityLogs.txt");
        }

        /// <summary>
        /// Fired when the form loads. Ensures the directory exists, then loads any existing entries.
        /// </summary>
        private void FrmLoginHistory_Load(object sender, EventArgs e)
        {
            try
            {
                string folder = Path.GetDirectoryName(_logFilePath);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                LoadLoginHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error initializing Login History view:\n{ex.Message}",
                    "Initialization Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Reads all lines from SecurityLogs.txt, parses each into five parts (UTC timestamp, UserNo, FullName, MAC, Details),
        /// converts the timestamp to local, and binds them to the DataGridView. If the file doesn’t exist, leaves the grid empty.
        /// </summary>
        private void LoadLoginHistory()
        {
            dgvLoginHistory.Rows.Clear();

            if (!File.Exists(_logFilePath))
            {
                return;
            }

            try
            {
                string[] allLines = File.ReadAllLines(_logFilePath);

                foreach (string rawLine in allLines)
                {
                    if (string.IsNullOrWhiteSpace(rawLine))
                        continue;

                    // Expect format: "timestamp | userNo | fullName | mac | details"
                    string[] parts = rawLine.Split(new[] { " | " }, 5, StringSplitOptions.None);

                    if (parts.Length == 5)
                    {
                        string timestampText = parts[0];
                        string userNoText = parts[1];
                        string fullNameText = parts[2];
                        string macText = parts[3];
                        string detailsText = parts[4];

                        if (DateTime.TryParse(timestampText, out DateTime utcTimestamp))
                        {
                            string local = utcTimestamp.ToLocalTime()
                                                       .ToString("yyyy-MM-dd HH:mm:ss");
                            dgvLoginHistory.Rows.Add(local, userNoText, fullNameText, macText, detailsText);
                        }
                        else
                        {
                            // If parsing fails, show raw timestamp
                            dgvLoginHistory.Rows.Add(timestampText, userNoText, fullNameText, macText, detailsText);
                        }
                    }
                    else
                    {
                        // Fallback if format is unexpected: show rawLine under Details only
                        dgvLoginHistory.Rows.Add(string.Empty, string.Empty, string.Empty, string.Empty, rawLine);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error reading login history file:\n{ex.Message}",
                    "Read Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Fired when the Refresh button is clicked: simply re-reads the file and repopulates the grid.
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadLoginHistory();
        }

        /// <summary>
        /// Fired when the Clear History button is clicked:
        /// prompts for confirmation, truncates the log file, and clears the grid.
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to permanently clear all login history?",
                "Confirm Clear",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Truncate the file to zero length (creates a new empty file if needed)
                    File.WriteAllText(_logFilePath, string.Empty);

                    // Clear the grid immediately
                    dgvLoginHistory.Rows.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Error clearing history:\n{ex.Message}",
                        "Clear Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }
    }
}
