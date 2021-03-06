﻿using System.Collections.Generic;

namespace Core.Model.Administration.Menu
{
    public class MenuJsonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuJsonModel"/> class.
        /// </summary>
        public MenuJsonModel()
        {
            this.Children = new List<Child>();
        }

        public string Path { get; set; }

        public string Icon { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Component { get; set; }

        public List<Child> Children { get; set; }
    }
}
