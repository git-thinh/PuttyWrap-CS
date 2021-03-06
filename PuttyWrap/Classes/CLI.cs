﻿/*
 * User: Amit Bagaria
 * Date: 12/28/2011
 * Time: 2:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PuttyWrap.Classes
{
	/// <summary>
	/// Description of CLI.
	/// </summary>
	public class CLI
{
		public static SessionData ParseCLIArguments(string[] args)
		{
			var sessionData = new SessionData();
			
			bool isUri = false;
            if (args.Length > 0)
            {
                sessionData = new SessionData();
                string proto = "", port = "", username = "", puttySession = "", password = "";

				if (args[0].StartsWith("ssh:"))
                {
                    args[0] = args[0].Remove(0, 6);
                    sessionData.Host = args[0];
                    sessionData.SessionName = args[0];
                    isUri = true;
                    args[args.Length - 1] = args[args.Length - 1].Remove(args[args.Length - 1].Length - 1, 1);
                }

                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i].ToString().ToLower())
                    {
                        case "-ssh":
                    		sessionData.Proto = ConnectionProtocol.SSH;
                            break;

                        case "-serial":
                            sessionData.Proto = ConnectionProtocol.Serial;
                            break;

                        case "-telnet":
                            sessionData.Proto = ConnectionProtocol.Telnet;
                            break;

                        case "-scp":
                            sessionData.Proto = ConnectionProtocol.SSH;
                            sessionData.UseSCP = true;
                            break;

                        case "-raw":
                            sessionData.Proto = ConnectionProtocol.Raw;
                            break;

                        case "-rlogin":
                            sessionData.Proto = ConnectionProtocol.Rlogin;
                            break;

                        case "-p":
                            port = args[i + 1];
                            i++;
                            break;

                        case "-l":
                            username = args[i + 1];
                            i++;
                            break;

                        case "-pw":
                            password = args[i + 1];
                            i++;
                            break;

                        case "-load":
                            puttySession = args[i + 1];
                            sessionData.PuttySession = args[i + 1];
                            i++;
                            break;
                    }
                }
                if (!isUri)
                {
                    sessionData.Host = args[args.Length - 1];
                    sessionData.SessionName = args[args.Length - 1];
                }

                sessionData.Port = (port != "") ? Convert.ToInt32(port) : 22;
                sessionData.Username = (username != "") ? username : Environment.UserName;
                sessionData.Password = (password != "") ? password : "";
                sessionData.PuttySession = (puttySession != "") ? puttySession : "Default%20Settings";
   			}

            return sessionData;
		}
		
		public CLI()
		{
		}
	}
}
