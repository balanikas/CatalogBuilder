using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Folding;

namespace DesktopClient
{
    class UIUtils
    {
        static FoldingManager _foldingManager;
        static XmlFoldingStrategy _foldingStrategy;

        public static void UpdateFolding(TextEditor textEditor)
        {
            if (_foldingManager == null)
            {
                _foldingManager = FoldingManager.Install(textEditor.TextArea);
            }
            _foldingStrategy = new XmlFoldingStrategy();

            textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.DefaultIndentationStrategy();

            _foldingStrategy.UpdateFoldings(_foldingManager, textEditor.Document);
        }


    }
}
