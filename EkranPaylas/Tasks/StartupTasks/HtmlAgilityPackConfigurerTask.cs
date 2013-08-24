﻿using HtmlAgilityPack;

namespace EkranPaylas.Tasks.StartupTasks
{
    public class HtmlAgilityPackConfigurerTask : IStartupTask
    {
        public int Priority
        {
            get { return 0; } 
        }

        public void Execute()
        {
            HtmlNode.ElementsFlags.Remove("form");
        }
    }
}