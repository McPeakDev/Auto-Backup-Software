///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  GSMAutoBackup / GSMAutoBackup
//	File Name:         ProgramForm.cs
//	Description:       Default Form for GSM Auto-Backup 
//	Author:            Matthew McPeak, McPeakML@etsu.edu
//  Copyright:         Matthew McPeak, 2020
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSMAutoBackup
{
    /// <summary>
    /// runs the File-Copy Program called GSM Auto-Backup
    /// </summary>
    public partial class ProgramForm : Form
    {
        /// <summary>
        /// Form Properties
        /// </summary>
        public string BaseDir { get; set; }
        public string FullDir { get; set; }
        public string DestDir { get; set; }
        public NotifyIcon notifyIcon { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ProgramForm()
        {
            //Create the Form
            InitializeComponent();

            //Define Default Properties
            BaseDir = Environment.OSVersion.Platform == PlatformID.Unix ? Environment.ExpandEnvironmentVariables("$HOME") : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
            FullDir = Environment.OSVersion.Platform == PlatformID.Unix ? $"{BaseDir}\\.gsmbackup" : $"{BaseDir}\\AppData\\Roaming\\.gsmbackup";
        }

        /// <summary>
        /// Shows a Folder Dialog and upon the user picking a directory it adds it to the list
        /// </summary>
        /// <param name="sender">The Form</param>
        /// <param name="e">The Event</param>
        private void addButton_Click(object sender, EventArgs e)
        {
            //Create a File Dialog and get the result.
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            //If the result is OK
            if(result == DialogResult.OK)
            {
                //If the list doesn't contain the directory already
                if (!filePathListBox.Items.Contains(fbd.SelectedPath))
                {
                    //Add the directory to the list
                    filePathListBox.Items.Add(fbd.SelectedPath);
                }
            }

        }

        /// <summary>
        /// Removes a selected Directory from the list.
        /// </summary>
        /// <param name="sender">The Form</param>
        /// <param name="e">The Event</param>
        private void removeButton_Click(object sender, EventArgs e)
        {
            filePathListBox.Items.Remove(filePathListBox.SelectedItem);
        }

        /// <summary>
        /// Saves the Backup Info. Runs a manual backup.
        /// </summary>
        /// <param name="sender">The Form</param>
        /// <param name="e">The Event</param>
        async private void manualRunButton_Click(object sender, EventArgs e)
        {
            //Hide the form.
            Hide();

            //Save the Backup Info.
            SaveData();

            //Enable the Timer.
            backupTimer.Enabled = true;

            //Create a new List for bakcup Directories.
            List<string> backupDirs = new List<string>();

            //Foreach directory in the list box.
            foreach (var item in filePathListBox.Items)
            {
                //Add the directory to the list.
                backupDirs.Add(item as string);
            }

            //If the Destination Directory is set.
            if (DestDir != String.Empty && DestDir != null)
            {
                //Run the backup in the background.
                await Task.Run(() => RecursiveBackup(backupDirs, DestDir));
            }
        }

        /// <summary>
        /// Allows the user to specify the backup destination directory.
        /// </summary>
        /// <param name="sender">The Form</param>
        /// <param name="e">The Event</param>
        private void destinationButton_Click(object sender, EventArgs e)
        {
            //Create a File Dialog and get the result.
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            //If the result is OK
            if (result == DialogResult.OK)
            {
                //Set the Destination Directory
                DestDir = fbd.SelectedPath;
            }
        }


        /// <summary>
        /// Allows the form to be shown again if the NotifyIcon is double clicked.
        /// </summary>
        /// <param name="sender">The Form</param>
        /// <param name="e">The Event</param>
        private void Icon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Set Window State to Default status
            WindowState = FormWindowState.Normal;
            
            //Show the Icon in the Taskbar.
            ShowInTaskbar = true;

            //Disable the backupTimer
            backupTimer.Enabled = false;

            //Show the Form.
            Show();
        }

        /// <summary>
        /// A timer that when the time interval is complete a backup is started.
        /// </summary>
        /// <param name="sender">The Form</param>
        /// <param name="e">The Event</param>
        async private void backupTimer_Tick(object sender, EventArgs e)
        {
            //Create a new List for the backup Directories
            List<string> backupDirs = new List<string>();

            //Foreach backup directory in the list box
            foreach (var item in filePathListBox.Items)
            {
                //Add the directory to the list.
                backupDirs.Add(item as string);
            }

            //If the Destination Directory is set
            if (DestDir != String.Empty && DestDir != null)
            {
                //Run the backup in the background.
                await Task.Run( () => RecursiveBackup(backupDirs, DestDir));
            }
        }

        /// <summary>
        /// Recursively backups a directory and its sub-directories
        /// </summary>
        /// <param name="backupDirs">The Directories to backup</param>
        /// <param name="prevDir">The Directory it was in previously</param>
        private void RecursiveBackup(List<string> backupDirs, string prevDir)
        {
            //Set the previous directory to the current Destination Directory.
            prevDir = DestDir;

            //Foreach backup directory in backupDirs
            foreach (var backupDir in backupDirs)
            {
                //If the backup directory is valid.
                if (backupDir != String.Empty)
                {
                    //Get the files in the current directory
                    var files = Directory.GetFiles(backupDir);

                    //Split the backup directory on the \\
                    var parts = backupDir.Split('\\');

                    //Set the current directory to the DestDir and the last part of the current backup directory. 
                    var currentDir = $"{DestDir}\\{parts[parts.Length - 1]}";

                    //if the current directory does not exist, then create it.
                    if (!Directory.Exists(currentDir))
                    {
                        Directory.CreateDirectory(currentDir);
                    }

                    //Create an empty list of errors
                    List<string> errors = new List<string>();

                    //Foreach file in files
                    foreach (var file in files)
                    {
                        //Get the fileName of the file
                        var fileName = Path.GetFileName(file);

                        //Define the destination file
                        var destFile = Path.Combine(currentDir, fileName);

                        //Try to copy the file.
                        try
                        {
                            File.Copy(file, destFile, true);
                        }
                        //Catch an errors
                        catch (Exception)
                        {
                            //Add the file's name to the error list.
                            errors.Add(fileName);
                        }
                    }

                    //If the error Count is greater than 0, create an error log on the user's desktop. Tell the user where it is.
                    if (errors.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("The following files were unable to be copied because either they were in use or the user did not have permission\n");
                        foreach (var error in errors)
                        {
                            sb.Append($"{error}\n");
                        }

                        File.WriteAllText($"{BaseDir}\\Desktop\\GSMErrorLog.txt", sb.ToString());

                        MessageBox.Show($"An error has occured please see {BaseDir}\\Desktop\\GSMErrorLog.txt for more info");
                    }

                    //Find any Sub-Directories
                    List<string> subDirectories = new List<string>(Directory.GetDirectories(backupDir));

                    //If the Sub-Directories list is not empty.
                    if (subDirectories.Count != 0)
                    {
                        //Set the Desination Dirrectory to the Current Directory
                        DestDir = currentDir;

                        //Call the RecursiveBackup again.
                        RecursiveBackup(subDirectories, prevDir);

                        //After the call is complete update the Destination Directory to its original form before the call.
                        DestDir = prevDir;
                    }
                }
            }
        }

        /// <summary>
        /// Save the Backup Info to a config file.
        /// </summary>
        private void SaveData()
        {
            //Create a List for Backup Directories
            List<string> backupDirs = new List<string>();

            //Add the Destination Dir to the top of the file.
            backupDirs.Add(DestDir);

            //Add each Backup Directory to the list.
            foreach (var item in filePathListBox.Items)
            {
                backupDirs.Add(item as string);
            }

            //If the Directory FullDir does not exist, create it.
            if (!Directory.Exists(FullDir))
            {
                Directory.CreateDirectory(FullDir);
            }

            //Write out each directory to the File.
            File.WriteAllLines($"{FullDir}\\config.gsm", backupDirs);
        }

        /// <summary>
        /// When the program is started it will start minimized with bakup enabled.
        /// </summary>
        /// <param name="e">The Event</param>
        async protected override void OnLoad(EventArgs e)
        {
            //Minimize and Hide the form.
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            Hide();
            
            //Set the Icon.
            Icon = new Icon("../../../Icons/GSM.ico");

            //Define the Notify Icon.
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Icon("../../../Icons/GSM.ico");
            notifyIcon.Visible = true;
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.BalloonTipTitle = "GSM Auto Backup";
            notifyIcon.BalloonTipText = "GSM Auto Backup has been minimized to the tray and while run continously in the background";
            notifyIcon.MouseDoubleClick += Icon_MouseDoubleClick;
            notifyIcon.ContextMenuStrip = new ContextMenuStrip();

            //Create and Define a new ToolStripMenu Item for the Notify Icon.
            ToolStripMenuItem saveItem = new ToolStripMenuItem();
            saveItem.Text = "Save";
            saveItem.Click += SaveItem_Click;

            //Create and Define a new ToolStripMenu Item for the Notify Icon.
            ToolStripMenuItem showItem = new ToolStripMenuItem();
            showItem.Text = "Show";
            showItem.Click += ShowItem_Click;

            //Create and Define a new ToolStripMenu Item for the Notify Icon.
            ToolStripMenuItem exitItem = new ToolStripMenuItem();
            exitItem.Text = "Exit";
            exitItem.Click += ExitItem_Click;

            //Add the ToolStripMenuItems to the notifyIcon's ContextMenuStrip.
            notifyIcon.ContextMenuStrip.Items.Add(showItem);
            notifyIcon.ContextMenuStrip.Items.Add(saveItem);
            notifyIcon.ContextMenuStrip.Items.Add(exitItem);

            //Show the notifyIcon.
            notifyIcon.ShowBalloonTip(2000);

            //If directory FullDir exists, read the config file.
            if (Directory.Exists(FullDir))
            {
                StreamReader sr = new StreamReader($"{FullDir}\\config.gsm");

                DestDir = sr.ReadLine();

                string line = sr.ReadLine();

                while (line != null)
                {
                    filePathListBox.Items.Add(line);
                    line = sr.ReadLine();
                }

                sr.Close();
            }

            //Create an empty list of backup directories
            List<string> backupDirs = new List<string>();

            //Foreach directory in the list, add the directory to the list.
            foreach (var item in filePathListBox.Items)
            {
                backupDirs.Add(item as string);
            }

            //If the Destination Directory is set.
            if (DestDir != String.Empty && DestDir != null)
            {
                //Run the backup in the background.
                await Task.Run(() => RecursiveBackup(backupDirs, DestDir));
            }

        }

        /// <summary>
        /// Closes the Application
        /// </summary>
        /// <param name="sender">The Form</param>
        /// <param name="e">The Event</param>
        private void ExitItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Shows the form.
        /// </summary>
        /// <param name="sender">The Form</param>
        /// <param name="e">The Event</param>
        private void ShowItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            Show();
        }

        /// <summary>
        /// Saves the Backup Info
        /// </summary>
        /// <param name="sender">The Form</param>
        /// <param name="e">The Event</param>
        private void SaveItem_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        /// <summary>
        /// Verify the form is closing for the correct reason.
        /// </summary>
        /// <param name="e">Form Close Event</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            //Verify the Close Reason
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                return;
            }
            else if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                SaveData();
            }
            else
            {
                e.Cancel = true;
                backupTimer.Enabled = true;
                Hide();
            }
        }
    }
}
