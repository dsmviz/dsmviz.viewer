﻿using Dsmviz.Interfaces.Util;

namespace Dsmviz.Viewer.ViewModel.Settings
{
    /// <summary>
    /// Settings used by dsm builder. Persisted in XML format using serialization.
    /// </summary>
    [Serializable]
    public class ViewerSettingsData
    {
        private LogLevel _logLevel;
        private bool _showCycles;
        private bool _betaFeaturesEnabled;
        private Theme _theme;

        public static ViewerSettingsData CreateDefault()
        {
            ViewerSettingsData settings = new ViewerSettingsData
            {
                LogLevel = LogLevel.None,
                ShowCycles = true,
                BetaFeaturesEnabled = false,
                Theme = Theme.Light,
            };

            return settings;
        }

        public LogLevel LogLevel
        {
            get => _logLevel;
            set => _logLevel = value;
        }

        public bool ShowCycles
        {
            get => _showCycles;
            set => _showCycles = value;
        }

        public bool BetaFeaturesEnabled
        {
            get => _betaFeaturesEnabled;
            set => _betaFeaturesEnabled = value;
        }

        public Theme Theme
        {
            get => _theme;
            set => _theme = value;
        }


    }
}

