using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Data;
using System.Speech.Recognition; // For Speech Recognition
using System.Globalization; // For CultureInfo

namespace Erp_V1
{
    public partial class frmchatbot1 : Form
    {
        private SpeechRecognitionEngine speechEngine = null;
        private bool isListening = false;

        public frmchatbot1()
        {
            InitializeComponent();
            InitializeSpeechRecognition(); // Initialize speech engine
            UpdateStatusLabel("Ready. Say 'Voice On' to enable voice commands.");
            txtChatDisplay.AppendText("Chatbot started. Type your message below, press Send/Enter, or use Voice Input.\r\n");
            //StartVoiceListening();
        }

        private void InitializeSpeechRecognition()
        {
            try
            {
                speechEngine = new SpeechRecognitionEngine(new CultureInfo("en-US")); // Or your desired culture
                speechEngine.LoadGrammar(CreateCommandsGrammar());
                speechEngine.SpeechRecognized += SpeechEngine_SpeechRecognized;
                speechEngine.SpeechRecognitionRejected += SpeechEngine_SpeechRecognitionRejected;
                speechEngine.SetInputToDefaultAudioDevice(); // Use default microphone
                speechEngine.RecognizeAsyncStop(); // Initially stopped
            }
            catch (Exception ex)
            {
                UpdateStatusLabel($"Speech Recognition Initialization Error: {ex.Message}");
                MessageBox.Show($"Speech Recognition failed to initialize. Voice input will be disabled. Error: {ex.Message}", "Speech Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnVoiceInput.Enabled = false; // Disable voice input button
            }
        }

        private Grammar CreateCommandsGrammar()
        {
            Choices commands = new Choices();
            commands.Add(new string[] {
                "voice on", "voice off", // Voice control commands
                "open camera", "start camera", "launch camera", "webcam",
                "open word", "launch word",
                "open calculator", "launch calculator", "open calc", "launch calc",
                "open notepad", "launch notepad",
                "cmd dir", "cmd ipconfig", "command systeminfo", "exec tasklist", // Example CMD commands
                "calculate", "calc", "what is", "compute", // Calculation commands
                "what is the date", "what time is it", // Date/Time commands
                "search web for", "google", "youtube", "wikipedia", // Web search commands
                "shutdown computer", "restart computer", "log off", // System control (use with caution!)
                "show system info", "system information", "get system details", // System info
                "adjust volume up", "volume up", "adjust volume down", "volume down", "mute volume", "unmute volume", // Volume control
                "open settings", "windows settings", "launch settings", // Open Settings
                "list processes", "show processes", "task list" // Task Management - Listing only for now (safer)
            });
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(commands);
            return new Grammar(gb);
        }


        private void SpeechEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string recognizedText = e.Result.Text.ToLowerInvariant();
            UpdateStatusLabel($"Recognized: '{recognizedText}'");
            HandleVoiceCommand(recognizedText); // Process recognized voice command
        }

        private void SpeechEngine_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            UpdateStatusLabel("Speech not recognized...");
        }


        private async void HandleVoiceCommand(string commandText)
        {
            if (commandText == "voice on")
            {
                StartVoiceListening();
                return; // Don't send "voice on" to Ollama
            }
            if (commandText == "voice off")
            {
                StopVoiceListening();
                return; // Don't send "voice off" to Ollama
            }

            // Map voice commands to text commands for existing processing logic
            string textCommand = commandText; // Default to sending recognized text as is

            if (commandText.StartsWith("search web for "))
            {
                textCommand = "search web for " + commandText.Substring("search web for ".Length);
            }
            else if (commandText.StartsWith("google "))
            {
                textCommand = "search web for " + commandText.Substring("google ".Length);
            }
            else if (commandText.StartsWith("youtube "))
            {
                textCommand = "search web for youtube " + commandText.Substring("youtube ".Length); // Specific search term for YouTube
            }
            else if (commandText.StartsWith("wikipedia "))
            {
                textCommand = "search web for wikipedia " + commandText.Substring("wikipedia ".Length); // Specific search term for Wikipedia
            }
            else if (commandText == "show system info" || commandText == "system information" || commandText == "get system details")
            {
                textCommand = "system info"; // Map to system info command
            }
            else if (commandText == "what is the date")
            {
                textCommand = "date";
            }
            else if (commandText == "what time is it")
            {
                textCommand = "time";
            }
            else if (commandText == "adjust volume up" || commandText == "volume up")
            {
                textCommand = "volume up";
            }
            else if (commandText == "adjust volume down" || commandText == "volume down")
            {
                textCommand = "volume down";
            }
            else if (commandText == "mute volume")
            {
                textCommand = "mute";
            }
            else if (commandText == "unmute volume")
            {
                textCommand = "unmute";
            }
            else if (commandText == "open settings" || commandText == "windows settings" || commandText == "launch settings")
            {
                textCommand = "open settings";
            }
            else if (commandText == "list processes" || commandText == "show processes" || commandText == "task list")
            {
                textCommand = "list processes";
            }
            else if (commandText == "shutdown computer")
            {
                textCommand = "shutdown computer";
            }
            else if (commandText == "restart computer")
            {
                textCommand = "restart computer";
            }
            else if (commandText == "log off")
            {
                textCommand = "log off";
            }


            txtUserInput.Text = textCommand; // Put recognized command into input box
            await SendMessage(); // Trigger SendMessage to process the command
        }


        private void StartVoiceListening()
        {
            if (!isListening)
            {
                speechEngine.RecognizeAsync(RecognizeMode.Multiple); // Start continuous recognition
                isListening = true;
                UpdateStatusLabel("Voice input active. Say commands...");
                btnVoiceInput.BackColor = System.Drawing.Color.Crimson; // Red to indicate listening
                btnVoiceInput.Text = "Stop Voice";
            }
        }

        private void StopVoiceListening()
        {
            if (isListening)
            {
                speechEngine.RecognizeAsyncStop();
                isListening = false;
                UpdateStatusLabel("Voice input stopped.");
                btnVoiceInput.BackColor = System.Drawing.Color.ForestGreen; // Green for voice ready
                btnVoiceInput.Text = "Voice";
            }
        }


        private void btnVoiceInput_Click(object sender, EventArgs e)
        {
            if (!isListening)
            {
                StartVoiceListening();
            }
            else
            {
                StopVoiceListening();
            }
        }


        private async Task SendMessage()
        {
            string userMessage = txtUserInput.Text.Trim().ToLowerInvariant();
            if (string.IsNullOrEmpty(userMessage)) return;

            txtChatDisplay.AppendText($"You: {txtUserInput.Text.Trim()}\r\n"); // Display original user input
            txtUserInput.Text = "";
            txtUserInput.Enabled = false;
            btnSend.Enabled = false;
            btnVoiceInput.Enabled = false; // Disable voice button during processing

            try
            {
                string botResponse = await ProcessUserCommand(userMessage);
                txtChatDisplay.AppendText($"Bot: {botResponse}\r\n");
            }
            catch (Exception ex)
            {
                txtChatDisplay.AppendText($"Error: {ex.Message}\r\n");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txtUserInput.Enabled = true;
                btnSend.Enabled = true;
                btnVoiceInput.Enabled = true; // Re-enable voice button
                txtUserInput.Focus();
            }
        }

        private async Task<string> ProcessUserCommand(string userMessage)
        {
            if (userMessage.StartsWith("cmd:", StringComparison.OrdinalIgnoreCase) ||
                userMessage.StartsWith("command:", StringComparison.OrdinalIgnoreCase) ||
                userMessage.StartsWith("exec:", StringComparison.OrdinalIgnoreCase))
            {
                string commandToExecute = userMessage.Substring(userMessage.IndexOf(':') + 1).Trim();
                return ExecuteWindowsCommand(commandToExecute);
            }
            else if (userMessage.StartsWith("open camera") || userMessage.StartsWith("start camera") || userMessage.StartsWith("launch camera") || userMessage.StartsWith("webcam"))
            {
                return OpenCameraApplication();
            }
            else if (userMessage.StartsWith("open word") || userMessage.StartsWith("launch word"))
            {
                return OpenApplication("word");
            }
            else if (userMessage.StartsWith("open calculator") || userMessage.StartsWith("launch calculator") || userMessage.StartsWith("open calc") || userMessage.StartsWith("launch calc"))
            {
                return OpenApplication("calculator");
            }
            else if (userMessage.StartsWith("open notepad") || userMessage.StartsWith("launch notepad"))
            {
                return OpenApplication("notepad");
            }
            else if (userMessage.StartsWith("calculate ") || userMessage.StartsWith("calc ") || userMessage.StartsWith("what is ") || userMessage.StartsWith("compute "))
            {
                string expression = "";
                if (userMessage.StartsWith("calculate ")) expression = userMessage.Substring("calculate ".Length).Trim();
                else if (userMessage.StartsWith("calc ")) expression = userMessage.Substring("calc ".Length).Trim();
                else if (userMessage.StartsWith("what is ")) expression = userMessage.Substring("what is ".Length).Trim();
                else if (userMessage.StartsWith("compute ")) expression = userMessage.Substring("compute ".Length).Trim();

                return PerformCalculation(expression);
            }
            else if (userMessage == "system info" || userMessage == "system information" || userMessage == "get system details" || userMessage == "show system info")
            {
                return GetSystemInformation();
            }
            else if (userMessage == "date" || userMessage == "what is the date")
            {
                return GetCurrentDate();
            }
            else if (userMessage == "time" || userMessage == "what time is it")
            {
                return GetCurrentTime();
            }
            else if (userMessage.StartsWith("search web for ") || userMessage.StartsWith("google ") || userMessage.StartsWith("youtube ") || userMessage.StartsWith("wikipedia "))
            {
                string searchTerm = "";
                if (userMessage.StartsWith("search web for ")) searchTerm = userMessage.Substring("search web for ".Length).Trim();
                else if (userMessage.StartsWith("google ")) searchTerm = userMessage.Substring("google ".Length).Trim();
                else if (userMessage.StartsWith("youtube ")) searchTerm = userMessage.Substring("youtube ".Length).Trim(); //Specific search for Youtube/Wikipedia handled in voice command mapping
                else if (userMessage.StartsWith("wikipedia ")) searchTerm = userMessage.Substring("wikipedia ".Length).Trim();

                return SearchTheWeb(searchTerm);
            }
            else if (userMessage == "shutdown computer")
            {
                return ShutdownComputer();
            }
            else if (userMessage == "restart computer")
            {
                return RestartComputer();
            }
            else if (userMessage == "log off")
            {
                return LogOffComputer();
            }
            else if (userMessage == "volume up" || userMessage == "adjust volume up")
            {
                return AdjustVolume(5); // Increase volume by 5% - adjust as needed
            }
            else if (userMessage == "volume down" || userMessage == "adjust volume down")
            {
                return AdjustVolume(-5); // Decrease volume by 5% - adjust as needed
            }
            else if (userMessage == "mute" || userMessage == "mute volume")
            {
                return MuteVolume();
            }
            else if (userMessage == "unmute" || userMessage == "unmute volume")
            {
                return UnmuteVolume();
            }
            else if (userMessage == "open settings" || userMessage == "windows settings" || userMessage == "launch settings")
            {
                return OpenWindowsSettings();
            }
            else if (userMessage == "list processes" || userMessage == "show processes" || userMessage == "task list")
            {
                return ListRunningProcesses();
            }


            else
            {
                return await SendMessageToOllama(userMessage); // Default to chat if no command matched
            }
        }


        private async Task<string> SendMessageToOllama(string message)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:11434/");
                string ollamaRequestEndpoint = "api/generate";

                var requestData = new
                {
                    model = "llama3.2",
                    prompt = message,
                    stream = false
                };
                string jsonData = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(ollamaRequestEndpoint, content);
                    response.EnsureSuccessStatusCode();

                    string responseContent = await response.Content.ReadAsStringAsync();
                    dynamic responseObject = JsonConvert.DeserializeObject(responseContent);

                    if (responseObject != null && responseObject.response != null)
                    {
                        return responseObject.response.ToString();
                    }
                    else
                    {
                        return "Error: Could not get a valid response from Ollama. Response structure unexpected.";
                    }
                }
                catch (HttpRequestException ex)
                {
                    return $"Error communicating with Ollama API: {ex.Message}. Is Ollama server running and accessible at http://localhost:11434?";
                }
                catch (JsonException ex)
                {
                    return $"Error parsing JSON response from Ollama: {ex.Message}. Response was: ... (check debug logs if available)";
                }
            }
        }

        private string ExecuteWindowsCommand(string command)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/c " + command;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;

                using (Process process = Process.Start(startInfo))
                {
                    if (process == null)
                    {
                        return "Error starting process.";
                    }
                    process.WaitForExit();

                    string output = process.StandardOutput.ReadToEnd();
                    string errorOutput = process.StandardError.ReadToEnd();

                    if (!string.IsNullOrEmpty(errorOutput))
                    {
                        return $"Command Error:\r\n{errorOutput}";
                    }
                    return $"Command Output:\r\n{output}";
                }
            }
            catch (Exception ex)
            {
                return $"Error executing command: {ex.Message}";
            }
        }

        private string OpenCameraApplication()
        {
            try
            {
                Process.Start("microsoft.windows.camera:");
                return "Opened Camera Application.";
            }
            catch (Exception ex)
            {
                return $"Error opening Camera: {ex.Message}.";
            }
        }

        private string OpenApplication(string appName)
        {
            string processName = "";
            string friendlyAppName = "";

            switch (appName.ToLower())
            {
                case "word":
                    processName = "winword.exe";
                    friendlyAppName = "Microsoft Word";
                    break;
                case "calculator":
                    processName = "calc.exe";
                    friendlyAppName = "Calculator";
                    break;
                case "notepad":
                    processName = "notepad.exe";
                    friendlyAppName = "Notepad";
                    break;
                case "settings":
                    processName = "ms-settings:"; // URI for Windows Settings App
                    friendlyAppName = "Windows Settings";
                    break;
                default:
                    return $"Unknown application: {appName}. Supported apps are: Word, Calculator, Notepad, Settings.";
            }

            try
            {
                Process.Start(processName);
                return $"Opened {friendlyAppName}.";
            }
            catch (Exception ex)
            {
                return $"Error opening {friendlyAppName}: {ex.Message}.";
            }
        }

        private string PerformCalculation(string expression)
        {
            try
            {
                DataTable dt = new DataTable();
                var result = dt.Compute(expression, "");
                return $"Result: {result}";
            }
            catch (Exception ex)
            {
                return $"Calculation Error: {ex.Message}. Please use a valid arithmetic expression.";
            }
        }

        private string GetSystemInformation()
        {
            OperatingSystem os = Environment.OSVersion;
            string version = os.VersionString;
            string machineName = Environment.MachineName;
            string userName = Environment.UserName;
            string systemDirectory = Environment.SystemDirectory;
            int processorCount = Environment.ProcessorCount;
            long memory = (long)new PerformanceCounter("Memory", "Available MBytes").NextValue();

            return $"System Information:\n" +
                   $"OS Version: {version}\n" +
                   $"Machine Name: {machineName}\n" +
                   $"User Name: {userName}\n" +
                   $"System Directory: {systemDirectory}\n" +
                   $"Processor Count: {processorCount}\n" +
                   $"Available Memory: {memory} MB";
        }

        private string GetCurrentDate()
        {
            return $"Current Date: {DateTime.Now.ToLongDateString()}";
        }

        private string GetCurrentTime()
        {
            return $"Current Time: {DateTime.Now.ToLongTimeString()}";
        }

        private string SearchTheWeb(string searchTerm)
        {
            try
            {
                string url = "https://www.google.com/search?q=" + Uri.EscapeDataString(searchTerm);
                Process.Start(url);
                return $"Searching the web for '{searchTerm}' in your default browser.";
            }
            catch (Exception ex)
            {
                return $"Error searching the web: {ex.Message}.";
            }
        }

        private string ShutdownComputer()
        {
            try
            {
                Process.Start("shutdown", "/s /t 0"); // /s for shutdown, /t 0 for immediate
                return "Shutting down computer...";
            }
            catch (Exception ex)
            {
                return $"Error shutting down: {ex.Message}.";
            }
        }

        private string RestartComputer()
        {
            try
            {
                Process.Start("shutdown", "/r /t 0"); // /r for restart, /t 0 for immediate
                return "Restarting computer...";
            }
            catch (Exception ex)
            {
                return $"Error restarting: {ex.Message}.";
            }
        }
        private string LogOffComputer()
        {
            try
            {
                Process.Start("shutdown", "/l /t 0"); // /l for logoff, /t 0 for immediate
                return "Logging off computer...";
            }
            catch (Exception ex)
            {
                return $"Error logging off: {ex.Message}.";
            }
        }

        private string AdjustVolume(int percentageChange)
        {
            try
            {
                //Requires NAudio library for proper volume control in .NET Framework (NuGet: NAudio)
                //This is a placeholder. Implement volume control using NAudio or other suitable library.
                //For simplicity, for now, just return a message indicating volume adjustment (without actual functionality)
                //In a real application, you'd use NAudio to interact with the system's audio mixer.

                //Example placeholder feedback:
                if (percentageChange > 0) return $"Volume increased by {percentageChange}%. (Functionality not fully implemented in this example.)";
                else if (percentageChange < 0) return $"Volume decreased by {Math.Abs(percentageChange)}%. (Functionality not fully implemented in this example.)";
                else return "Volume adjustment command received. (Functionality not fully implemented in this example.)";


                // **Example using NAudio (requires NuGet package - NAudio):**
                // **Note:** This is a simplified example and might need adjustments for proper error handling and finer control.
                /*
                using (var sessionManager = new AudioSessionManager())
                {
                    float currentVolume = sessionManager.MasterVolume; // Get current master volume (0 to 1)
                    float newVolume = Math.Max(0, Math.Min(1, currentVolume + (percentageChange / 100.0f))); // Calculate new volume, clamp to 0-1
                    sessionManager.MasterVolume = newVolume; // Set new master volume
                    return $"Volume adjusted to {(int)(newVolume * 100)}%.";
                }
                */
            }
            catch (Exception ex)
            {
                return $"Error adjusting volume: {ex.Message}. (Volume control functionality might require additional libraries like NAudio.)";
            }
        }


        private string MuteVolume()
        {
            try
            {
                // Requires NAudio library (NuGet: NAudio) for proper system-wide mute/unmute.
                // Placeholder message for now.
                return "Volume muted. (Mute/Unmute functionality not fully implemented in this example.)";

                // **Example using NAudio (requires NuGet package - NAudio):**
                /*
                using (var sessionManager = new AudioSessionManager())
                {
                    sessionManager.MuteMasterVolume();
                    return "Volume muted.";
                }
                */
            }
            catch (Exception ex)
            {
                return $"Error muting volume: {ex.Message}. (Mute/Unmute might require NAudio library.)";
            }
        }

        private string UnmuteVolume()
        {
            try
            {
                // Requires NAudio library (NuGet: NAudio) for proper system-wide mute/unmute.
                // Placeholder message for now.
                return "Volume unmuted. (Mute/Unmute functionality not fully implemented in this example.)";

                // **Example using NAudio (requires NuGet package - NAudio):**
                /*
                using (var sessionManager = new AudioSessionManager())
                {
                    sessionManager.UnMuteMasterVolume();
                    return "Volume unmuted.";
                }
                */
            }
            catch (Exception ex)
            {
                return $"Error unmuting volume: {ex.Message}. (Mute/Unmute might require NAudio library.)";
            }
        }
        private async void btnSend_Click(object sender, EventArgs e)
        {
            await SendMessage();
        }

        private async void txtUserInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                await SendMessage();
            }
        }
        private string OpenWindowsSettings()
        {
            return OpenApplication("settings"); // Reuse OpenApplication for settings using URI
        }

        private string ListRunningProcesses()
        {
            try
            {
                Process[] processes = Process.GetProcesses();
                StringBuilder processList = new StringBuilder();
                processList.AppendLine("Running Processes:");
                foreach (var process in processes)
                {
                    try
                    {
                        processList.AppendLine($"- {process.ProcessName} (ID: {process.Id})");
                    }
                    catch (Exception)
                    {
                        processList.AppendLine($"- {process.ProcessName} (ID: {process.Id}) - Access Denied to details");
                    }
                }
                return processList.ToString();
            }
            catch (Exception ex)
            {
                return $"Error listing processes: {ex.Message}";
            }
        }


        private void UpdateStatusLabel(string statusText)
        {
            lblStatus.Text = "Status: " + statusText;
        }
    }
}