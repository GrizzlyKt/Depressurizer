using MaterialSkin;
using MaterialSkin.Controls;

namespace Depressurizer
{
    public partial class MainForm : MaterialForm
    {
        #region Constructors and Destructors

        public MainForm()
        {
            InitializeComponent();
            InitializeMaterialSkin();
        }

        #endregion

        #region Properties

        private static MaterialSkinManager MaterialSkinManager => MaterialSkinManager.Instance;

        #endregion

        #region Methods

        private void InitializeMaterialSkin()
        {
            MaterialSkinManager.AddFormToManage(this);
            MaterialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            MaterialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        #endregion
    }
}
