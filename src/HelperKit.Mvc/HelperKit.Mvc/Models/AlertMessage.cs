namespace HelperKit.Mvc
{
    #region MESSAGE TYPE

    public enum MessageType
    {
        Primary,
        Secondary,
        Success,
        Danger,
        Warning,
        Info,
        Light,
        Dark,
        Default
    }

    #endregion

    #region MODAL

    public enum ModalSize
    {
        Normal,
        Small,
        Large
    }

    #endregion

    public enum DataPlacement
    {
        top,
        right,
        bottom,
        left
    }

    public class AlertMessage : IAlertMessage
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public MessageType MessageType { get; set; }
        public bool IsDismissible { get; set; }
    }

    public interface IAlertMessage
    {
        string Title { get; set; }
        string Body { get; set; }
        MessageType MessageType { get; set; }
        bool IsDismissible { get; set; }
    }
}