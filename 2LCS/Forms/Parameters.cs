using System;
using System.Windows.Forms;
using LCS.Cache;

namespace LCS.Forms
{
    public partial class Parameters : Form
    {
        public Parameters()
        {
            InitializeComponent();
        }

        public bool Autorefresh { get; private set; }
        public bool Cancelled { get; private set; }
        public bool CachingEnabled { get; private set; }
        public bool StoreCache { get; private set; }

        // DXC mlitwin2@dxc.com: new fields in the grid -begin

        public int  QueryTimeout {  get; private set; }

        // DXC mlitwin2@dxc.com: new fields in the grid -end

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            SetParameters();
            Close();
        }

        private void Parameters_Load(object sender, EventArgs e)
        {
            LoadParameters();
        }

        private void LoadParameters()
        {
            AutoRefreshCheckBox.Checked = Properties.Settings.Default.autorefresh;
            minimizeToNotificationArea.Checked = Properties.Settings.Default.minimizeToNotificationArea;
            textBoxProjectExcl.Text = Properties.Settings.Default.projOrgExcl;
            CachingEnabledCheckbox.Checked = Properties.Settings.Default.cachingEnabled;
            StoreCacheCheckBox.Checked = Properties.Settings.Default.keepCache;

            // DXC mlitwin2@dxc.com: new fields in the grid -begin

            QueryTimeoutTextBox.Text = Properties.Settings.Default.queryTimeout;

            // DXC mlitwin2@dxc.com: new fields in the grid -end

            SetStoreCacheEnabledDisabled();
        }

        private void SetParameters()
        {
            Properties.Settings.Default.autorefresh = AutoRefreshCheckBox.Checked;
            Properties.Settings.Default.minimizeToNotificationArea = minimizeToNotificationArea.Checked;
            Properties.Settings.Default.projOrgExcl = textBoxProjectExcl.Text;
            Properties.Settings.Default.cachingEnabled = CachingEnabledCheckbox.Checked;
            Properties.Settings.Default.keepCache = StoreCacheCheckBox.Checked;

            // DXC mlitwin2@dxc.com: new fields in the grid -begin

            Properties.Settings.Default.queryTimeout = QueryTimeoutTextBox.Text;

            // DXC mlitwin2@dxc.com: new fields in the grid -end

            Properties.Settings.Default.Save();
        }

        private void CachingEnabledCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            SetStoreCacheEnabledDisabled();
        }

        private void SetStoreCacheEnabledDisabled()
        {
            if (CachingEnabledCheckbox.Checked)
            {
                StoreCacheCheckBox.Enabled = true;
            }
            else
            {
                StoreCacheCheckBox.Enabled = false;
                StoreCacheCheckBox.Checked = false;
                CredentialsCacheHelper.DisableCache();
            }
        }

        private void ClearCacheButton_Click(object sender, EventArgs e)
        {
            var result = CredentialsCacheHelper.ClearCache();

            if(result != null)
            {
                MessageBox.Show(string.Format("Error while trying to clear cache, caching disabled.\n {0}", result), "Error");
            }
        }        
    }
}