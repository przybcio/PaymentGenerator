﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Management.Automation.Runspaces;
using System.Management.Automation;


namespace PaymentsGenerator
{
    public class ElixirGenThread
    {
        internal void Start()
        {
            Thread runspaceThread = new Thread(() =>
            {
                using (Runspace rs = RunspaceFactory.CreateRunspace())
                {
                    rs.Open();
                    using (PowerShell ps = PowerShell.Create())
                    {
                        ps.Runspace = rs;
                        ps.AddScript(Utils.GetScript(Script));                        
                        ps.AddArgument(noOfFilesParam);
                        ps.AddArgument(noOfRecordsParam);
                        string path = Utils.GetCurrentAppPathDirectory();
                       
                        ps.AddArgument(path + OutputFolder);
                        IAsyncResult async = ps.BeginInvoke();

                        var output = ps.EndInvoke(async);
                        PaymentsGenerator.MainWindow.UpdateContext deleg = mainWindow.MainWindowDeleg;

                        
                        StringBuilder sb = new StringBuilder();
                        foreach (var item in output)
                        {
                            sb.AppendLine(item.BaseObject.ToString());
                        }
                        mainWindow.Dispatcher.Invoke(deleg, sb.ToString());

                    }
                }
            });
            runspaceThread.Start();
        }

        
        public ElixirGenThread(MainWindow mainWindow)
        {           
            this.mainWindow = mainWindow;
        }

        public ElixirGenThread(MainWindow mainWindow, int noOfFilesParam, int noOfRecordsParam)
        {
            // TODO: Complete member initialization
            this.mainWindow = mainWindow;
            this.noOfFilesParam = noOfFilesParam;
            this.noOfRecordsParam = noOfRecordsParam;
        }

        private const string Script = @"scripts/generate_elixir.ps1";
        private const string OutputFolder = @"\elixir";
        private MainWindow mainWindow;
        private int noOfFilesParam;
        private int noOfRecordsParam;

    }
}
