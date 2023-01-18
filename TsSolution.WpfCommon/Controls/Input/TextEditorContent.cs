using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsSolutions.WpfCommon.Controls.Input
{
    public class TextEditorContent
    {
        public TextEditorContentType Type { get; internal set; }

        public String Content { get; internal set; }

        private TextEditorContent(TextEditorContentType type, String content)
        {
            Type = type;
            Content = content;
        }

        public static TextEditorContent Create(TextEditorContentType type, String content)
        {
            return new TextEditorContent(type, content);
        }
    }
}