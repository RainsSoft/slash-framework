﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggerExtensions.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.Common.Logging
{
    using UnityEngine;

    public static class LoggerExtensions
    {
        /// <summary>
        ///   Call this method to initialize the logger to use it with Unity.
        /// </summary>
        public static void InitLogger()
        {
#if !UNITY_5_5_OR_NEWER
            if (Application.platform == RuntimePlatform.OSXWebPlayer
                || Application.platform == RuntimePlatform.WindowsWebPlayer)
            {
                // logging won't make a lot of sense in a Web player...
                return;
            }
#endif

            var configFile = Application.dataPath + "/Configurations/log4net.xml";
            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                configFile = Application.dataPath + "\\Configurations\\log4net.xml";
            }

            Debug.Log(string.Format("Init logger with configuration {0}", configFile));

            Logger.Configure(configFile);
        }
    }
}