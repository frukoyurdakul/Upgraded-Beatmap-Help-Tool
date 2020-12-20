using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.Utils
{
    public class HtmlUtils
    {
        public static HtmlDisplayer newHtmlDisplayer()
        {
            return new HtmlDisplayer();
        }

        public class HtmlDisplayer
        {
            private StringBuilder builder;
            private bool hasElements = false;
            private bool hasSections = false;
            private bool hasSubsections = false;

            internal HtmlDisplayer()
            {
                builder = new StringBuilder();
                builder.Append("<!DOCTYPE html><html><head><style>p{margin:1px},p.warning{margin:1px;color:#e53935},p.section{margin:1px;color:#0d47a1},p.subsection{margin:1px;color:#2e7d32}</style></head><body>");
            }

            public bool containsElements()
            {
                return hasElements;
            }

            public bool containsSections()
            {
                return hasSections;
            }

            public bool containsSubsections()
            {
                return hasSubsections;
            }

            public void addLineBreak()
            {
                builder.Append("</br>");
            }

            public void addLineWithBreak(string line)
            {
                hasElements = true;
                builder.Append("<p>").Append(line).Append("</p></br>");
            }

            public void addLine(string line)
            {
                hasElements = true;
                builder.Append("<p>").Append(line).Append("</p>");
            }

            public void addLines(params string[] values)
            {
                foreach (string line in values)
                    addLine(line);
            }

            public void addLinesWithBreaks(params string[] values)
            {
                foreach (string line in values)
                    addLineWithBreak(line);
            }

            public void addWarnings(params string[] values)
            {
                foreach (string line in values)
                    addWarning(line);
            }

            public void addWarning(string line)
            {
                hasElements = true;
                builder.Append("<p class=\"warning\">").Append(line).Append("</p>");
            }

            public void addSection(string line)
            {
                hasElements = true;
                hasSections = true;
                builder.Append("<p class=\"section\">").Append(line).Append("</p>");
                addLineBreak();
            }

            public void addSubsection(string line)
            {
                hasElements = true;
                hasSubsections = true;
                builder.Append("<p class=\"subsection\">").Append(line).Append("</p>");
            }

            public void addLine(string line, bool warning)
            {
                if (warning)
                    addWarning(line);
                else
                    addLine(line);
            }

            public void recycle()
            {
                builder.Clear();
                hasElements = false;
            }

            public override string ToString()
            {
                return builder.Append("</body></html>").ToString();
            }
        }
    }
}
