﻿using System.Collections.Generic;
using Core.Model.Administration.Menu;
using Core.Mvc.Areas.Administration.Routes;
using Core.Web.Button;
using Core.Web.Dialog;
using Core.Web.Html;
using Core.Web.TextBox;
using Resources = Core.Mvc.Resource.Areas.Administration.ViewConfiguration.Menu.EditMenuDialogConfigurationResource;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Menu
{
    public class EditMenuDialogConfiguration<TPostModel, TModel> : DialogConfiguration<TPostModel, TModel>
        where TPostModel : MenuEditPostModel
        where TModel : MenuModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditMenuDialogConfiguration{TPostModel, TModel}"/> class.
        /// </summary>
        /// <param name="menu">The Menu.</param>
        public EditMenuDialogConfiguration(TModel menu) : base(MenuIdentifiers.EditMenuDialogIdentifier, menu)
        {
        }

        public override string Title => Resources.EditMenuTitle;

        protected override void CreateHiddenValues(IList<IHtmlContent<TPostModel, TModel>> textBoxes)
        {
            textBoxes.Add(new HiddenValue<TPostModel, TModel>(o => o.Id, this.Model.Id));
        }

        protected override void CreateBody(IList<IHtmlContent<TPostModel, TModel>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>(Resources.Name, o => o.Name, o => o.Name));
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>(Resources.Url, o => o.Url, o => o.Url));
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>(Resources.Description, o => o.Description, o => o.Description));
            textBoxes.Add(new LabeledIntegerBox<TPostModel, TModel>(Resources.Sort, o => o.Sort, o => o.Sort));
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>(Resources.Alias, o => o.Alias, o => o.Alias));
        }

        protected override void CreateButtons(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton(Resources.Submit, "index.submit", MenuRoute.SaveEdit));
            buttons.Add(new StandardButton(Resources.Cancel, "core.cancel"));
        }
    }
}