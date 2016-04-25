"use strict";

module Affecto
{
    export class HtmlContent
    {
        private static get newLineElement(): string
        {
            return "<br/>";
        }

        public static escapeAndReplaceNewLines(content: string): string
        {
            return this.replaceNewLines(this.escape(content));
        }

        public static escape(content: string): string
        {
            if (content != null)
            {
                var div = document.createElement("div");
                div.appendChild(document.createTextNode(content));
                return div.innerHTML;
            }
            return null;
        }

        public static  unescape(content: string): string
        {
            if (content != null)
            {
                var e = document.createElement("div");
                e.innerHTML = content;
                return e.childNodes.length === 0 ? "" : e.childNodes[0].nodeValue;                
            }
            return null;
        }

        public static wrapSorted(lines: Array<string>): string
        {
            var content = "";
            if (lines != null)
            {
                lines.sort();
                lines.forEach((line: string) =>
                {
                    if(line != null && line !== "")
                    {
                        content += this.escape(line) + this.newLineElement;
                    }
                });
                content = this.removeLastNewLine(content);
            }
            return content;
        }

        public static replaceNewLines(content: string): string
        {
            if (content == null)
            {
                return null;
            }
            return content.replace(/(?:\r\n|\r|\n)/g, this.newLineElement);
        }

        private static removeLastNewLine(content: string): string
        {
            if (content !== "")
            {
                return content.substring(0, content.length - this.newLineElement.length);
            }
            return content;
        }
    }
}