﻿using Core.Model.Administration.User;
using Core.Web.Button;
using Core.Web.Identifiers;
using Core.Web.TextBox;
using Core.Web.ViewConfiguration;
using System.Collections.Generic;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class AddUserDialogConfiguration : DialogConfiguration<UserCreatePostModel, User>
    {
        public static Identifier Identifier { get; } = new Identifier();

        /// <summary>
        /// 构造函数
        /// </summary>
        public AddUserDialogConfiguration() : base(null, Identifier)
        {
        }

        public override string Title => "添加用户";

        protected override void CreateBody(IList<LabeledTextBox<UserCreatePostModel, User>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<UserCreatePostModel, User>("登录名", o => o.LoginName));
            textBoxes.Add(new LabeledTextBox<UserCreatePostModel, User>("显示名", o => o.DisplayName));
            textBoxes.Add(new LabeledTextBox<UserCreatePostModel, User>("密码", o => o.Password, null, TextBoxTypeEnum.password));
        }

        protected override void CreateButtons(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton("提交", new Identifier(), "index.submit"));
        }
    }
}