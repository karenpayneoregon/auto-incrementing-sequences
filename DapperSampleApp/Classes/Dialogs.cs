using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperSampleApp.Classes;
internal class Dialogs
{
    public static void ErrorBox(Control owner, Exception exception, string buttonText = "OK")
    {

        TaskDialogButton button = new(buttonText);

        var text = $"Encountered the following{Environment.NewLine}{exception.Message}";

        Bitmap iconBitmap;
        using (var ms = new MemoryStream(Properties.Resources.Explaination))
        {
            iconBitmap = new Bitmap(ms);
        }

        TaskDialogPage page = new()
        {
            Caption = "Error",
            SizeToContent = true,
            Heading = text,
            Icon = new TaskDialogIcon(iconBitmap),
            Buttons = [button]
        };

        TaskDialog.ShowDialog(owner, page);

    }
}
