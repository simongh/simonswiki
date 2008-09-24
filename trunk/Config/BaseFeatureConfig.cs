using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Wiki_Config
{
    public abstract class BaseFeatureConfig : ConfigurationElement
    {
        public const string ProviderKey = "provider";

        [ConfigurationProperty(BaseFeatureConfig.ProviderKey)]
        public string Provider
        {
            get { return (string)this[ProviderKey]; }
            set { this[ProviderKey] = value; }
        }
    }
}
