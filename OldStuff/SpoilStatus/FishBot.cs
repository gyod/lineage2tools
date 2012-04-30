using System;
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace SpoilStatus
{
    /// <summary>
    /// Benutzt WSH Um die Tasten zu senden! GameGuard hookt zwar alle Möglichen Funktionen um SendKey, PostMessage, usw...
    /// Aber wer hätte gedacht das er WSH einfach so ignoriert ^^
    /// </summary>
    class FishBot
    {
        private readonly WshShell WSH = new WshShell();
        private object app;

        private object BF = false;
        private object wait = true;

        private string reelBtn;
        private string pumpBtn;
        private string shotBtn;

        private DateTime lastReel = DateTime.Now;
        private DateTime lastPump = DateTime.Now;
        private bool shotLoaded = false;

        private TimeSpan reuseTime = TimeSpan.FromMilliseconds(OptionsForm.Instance.FishingSkillReuseTime);

        /// <summary>
        /// see @http://www.ss64.com/wsh/sendkeys.html for more Keys
        /// </summary>
        /// <param name="windowName">zb. Lineage II</param>
        public FishBot(string windowName)
        {
            this.app = windowName;
            this.ReelBtn = OptionsForm.Instance.ReelKey;
            this.PumpBtn = OptionsForm.Instance.PumpKey;
            this.ShotBtn = OptionsForm.Instance.FishshotKey;

            OptionsForm.Instance.OnPropertiesChanged += new OptionsForm.PropertiesChangedEventHandler(OnPropertiesChanged);
#if DEBUG
            Program.debugStream.WriteLine("Fishbot Initialized: " + windowName + " " + reelBtn + " " + pumpBtn);
            Program.debugStream.WriteLine("Fishbot getFocus: " + GetFocus());
#endif
        }

        void OnPropertiesChanged(object sender)
        {
            this.PumpBtn = OptionsForm.Instance.PumpKey;
            this.ReelBtn = OptionsForm.Instance.ReelKey;
            this.ShotBtn = OptionsForm.Instance.FishshotKey;
            this.reuseTime = TimeSpan.FromMilliseconds(OptionsForm.Instance.FishingSkillReuseTime);
        }

        private bool GetFocus()
        {
            return WSH.AppActivate(ref app, ref BF);
        }

        public void UseReel()
        {
            if ((DateTime.Now - lastReel) >= ReuseTime)
            {
                LoadShot();
                lastReel = DateTime.Now;
                WSH.AppActivate(ref app, ref BF);
                WSH.SendKeys(this.reelBtn, ref wait);
                this.shotLoaded = false;
            }
#if DEBUG
            Program.debugStream.WriteLine("UseReel");
#endif
        }

        public void UsePump()
        {
            if ((DateTime.Now - lastPump) >= ReuseTime)
            {
                LoadShot();
                lastPump = DateTime.Now;
                WSH.AppActivate(ref app, ref BF);
                WSH.SendKeys(this.pumpBtn, ref wait);
                this.shotLoaded = false;
            }
#if DEBUG
            Program.debugStream.WriteLine("UsePump");
#endif
        }

        private void LoadShot()
        {
            if (OptionsForm.Instance.UseFishingshots && !this.shotLoaded)
            {
                WSH.AppActivate(ref app, ref BF);
                WSH.SendKeys(this.shotBtn, ref wait);
                this.shotLoaded = true;
            }
        }

        #region Public getter/setter


        public Keys PumpBtn
        {
            set { pumpBtn = "{" + value + "}"; }
        }

        public Keys ReelBtn
        {
            set { reelBtn = "{" + value + "}"; }
        }

        public Keys ShotBtn
        {
            set { shotBtn = "{" + value + "}"; }
        }

        public TimeSpan ReuseTime
        {
            get { return reuseTime; }
            set { reuseTime = value; }
        }

        #endregion
    }
}
