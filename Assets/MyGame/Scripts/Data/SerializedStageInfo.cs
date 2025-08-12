namespace Base.Data
{
    public struct SerializedStageInfo
    {
        public string StageName;
        public string IconName;
        public bool Unlocked;

        public SerializedStageInfo(string stageName, string iconName, bool unlocked)
        {
            StageName = stageName;
            IconName = iconName;
            Unlocked = unlocked;
        }

        public void Unlock()
        {
            Unlocked = true;
        }
    }
}
