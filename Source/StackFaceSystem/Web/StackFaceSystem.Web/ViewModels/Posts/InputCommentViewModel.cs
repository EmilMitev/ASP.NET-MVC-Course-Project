﻿namespace StackFaceSystem.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class InputCommentViewModel
    {
        [AllowHtml]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string Content { get; set; }
    }
}
