using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorLib.Resources.Icons
{
    /// <summary>
    /// Icons from => https://fonts.google.com/icons?icon.set=Material+Icons&icon.query=script&icon.platform=web
    /// </summary>
    public static class Material
    {
        public static Geometry Description = Geometry.Parse("M0 0h24v24H0V0z M14.59 2.59c-.38-.38-.89-.59-1.42-.59H6c-1.1 0-2 .9-2 2v16c0 1.1.89 2 1.99 2H18c1.1 0 2-.9 2-2V8.83c0-.53-.21-1.04-.59-1.41l-4.82-4.83zM15 18H9c-.55 0-1-.45-1-1s.45-1 1-1h6c.55 0 1 .45 1 1s-.45 1-1 1zm0-4H9c-.55 0-1-.45-1-1s.45-1 1-1h6c.55 0 1 .45 1 1s-.45 1-1 1zm-2-6V3.5L18.5 9H14c-.55 0-1-.45-1-1z");

        public static Geometry ArrowCircleLeft = Geometry.Parse("M2,12c0,5.52,4.48,10,10,10c5.52,0,10-4.48,10-10S17.52,2,12,2C6.48,2,2,6.48,2,12z M20,12c0,4.42-3.58,8-8,8 c-4.42,0-8-3.58-8-8s3.58-8,8-8C16.42,4,20,7.58,20,12z M8,12l4-4l1.41,1.41L11.83,11H16v2h-4.17l1.59,1.59L12,16L8,12z");

        public static Geometry AddCircleOutline = Geometry.Parse("M13 7h-2v4H7v2h4v4h2v-4h4v-2h-4V7zm-1-5C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z");

        public static Geometry DeleteOutline = Geometry.Parse("M6 19c0 1.1.9 2 2 2h8c1.1 0 2-.9 2-2V7H6v12zM8 9h8v10H8V9zm7.5-5l-1-1h-5l-1 1H5v2h14V4h-3.5z");

        public static Geometry Clear = Geometry.Parse("M19 6.41L17.59 5 12 10.59 6.41 5 5 6.41 10.59 12 5 17.59 6.41 19 12 13.41 17.59 19 19 17.59 13.41 12 19 6.41z");
        /*
        public static Geometry xxx = Geometry.Parse("");
        public static Geometry xxx = Geometry.Parse("");
        public static Geometry xxx = Geometry.Parse("");
        public static Geometry xxx = Geometry.Parse("");
        public static Geometry xxx = Geometry.Parse("");
        public static Geometry xxx = Geometry.Parse("");
        public static Geometry xxx = Geometry.Parse("");*/
    }
}