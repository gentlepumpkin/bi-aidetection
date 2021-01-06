using System;

namespace NPushover.RequestObjects
{
    /// <summary>
    /// Represents an Pushover message that can be sent using the <see cref="Pushover"/> class.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class with default <see cref="Priority"/> and
        /// <see cref="Sound"/>.
        /// </summary>
        public Message()
            : this(Priority.Normal, null, null, false, Sounds.Pushover) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class with default <see cref="Priority"/> and with
        /// the specified <see cref="Sound"/>.
        /// </summary>
        /// <param name="sound">The <see cref="Sound"/> of the <see cref="Message"/>.</param>
        public Message(Sounds sound)
            : this(Priority.Normal, null, sound) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class with default <see cref="Priority"/> and
        /// <see cref="Sound"/> and with the specified <see cref="Body"/>.
        /// </summary>
        /// <param name="body">The <see cref="Message"/> <see cref="Body"/>.</param>
        public Message(string body)
            : this(Priority.Normal, body) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class with default <see cref="Sound"/> and with the
        /// specified <see cref="Priority"/> and <see cref="Body"/>.
        /// </summary>
        /// <param name="priority">The <see cref="Message"/> <see cref="Priority"/>.</param>
        /// <param name="body">The <see cref="Message"/> <see cref="Body"/>.</param>
        public Message(Priority priority, string body)
            : this(priority, body, Sounds.Pushover) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class with the specified <see cref="Priority"/>, 
        /// <see cref="Body"/> and <see cref="Sound"/>.
        /// </summary>
        /// <param name="priority">The <see cref="Message"/> <see cref="Priority"/>.</param>
        /// <param name="body">The <see cref="Message"/> <see cref="Body"/>.</param>
        /// <param name="sound">The <see cref="Sound"/> of the <see cref="Message"/>.</param>
        public Message(Priority priority, string body, Sounds sound)
            : this(priority, body, false, sound) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class with the specified <see cref="Priority"/>, 
        /// <see cref="Body"/> and <see cref="Sound"/>.
        /// </summary>
        /// <param name="priority">The <see cref="Message"/> <see cref="Priority"/>.</param>
        /// <param name="body">The <see cref="Message"/> <see cref="Body"/>.</param>
        /// <param name="isHtmlBody">When the <see cref="Body"/> contains HTML, set to true. False otherwise. See <see cref="IsHtmlBody"/>.</param>
        /// <param name="sound">The <see cref="Sound"/> of the <see cref="Message"/>.</param>
        public Message(Priority priority, string body, bool isHtmlBody, Sounds sound)
            : this(priority, null, body, isHtmlBody, sound) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class with the specified <see cref="Priority"/>, 
        /// <see cref="Title"/>, <see cref="Body"/> and <see cref="Sound"/>.
        /// </summary>
        /// <param name="priority">The <see cref="Message"/> <see cref="Priority"/>.</param>
        /// <param name="title">The <see cref="Title"/> for the <see cref="Message"/>.</param>
        /// <param name="body">The <see cref="Message"/> <see cref="Body"/>.</param>
        /// <param name="isHtmlBody">When the <see cref="Body"/> contains HTML, set to true. False otherwise. See <see cref="IsHtmlBody"/>.</param>
        /// <param name="sound">The <see cref="Sound"/> of the <see cref="Message"/>.</param>
        public Message(Priority priority, string title, string body, bool isHtmlBody, Sounds sound)
        {
            this.Priority = priority;
            this.Title = title;
            this.Body = body;
            this.IsHtmlBody = IsHtmlBody;
            this.SetSound(sound);
        }

        /// <summary>
        /// Gets/sets the <see cref="Priority"/> of the <see cref="Message"/>.
        /// </summary>
        /// <remarks>
        /// When you send a <see cref="Message"/> with priority <see cref="NPushover.RequestObjects.Priority.Emergency"/>, 
        /// Pushover will respond with a <see cref="NPushover.ResponseObjects.PushoverResponse"/> containing a
        /// <see cref="P:NPushover.ResponseObjects.PushoverResponse.Receipt"/> that can be used to get information
        /// about whether the <see cref="Message"/> has been acknowledged.
        /// </remarks>
        /// <seealso cref="M:NPushover.Pushover.AcknowledgeMessageAsync(System.String,System.String)"/>
        /// <seealso cref="M:NPushover.Pushover.GetReceiptAsync(System.String)"/>
        public Priority Priority { get; set; }

        /// <summary>
        /// Gets/sets the (optional) title of the <see cref="Message"/>.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets/sets the body of the <see cref="Message"/>; may contain some HTML. See <see cref="IsHtmlBody"/>.
        /// </summary>
        /// <seealso href="https://pushover.net/api#html">Pushover API documentation</seealso>
        public string Body { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="SupplementaryURL"/> of the <see cref="Message"/>.
        /// </summary>
        /// <seealso href="https://pushover.net/api#urls">Pushover API documentation</seealso>
        /// <seealso cref="SupplementaryURL"/>
        public SupplementaryURL SupplementaryUrl { get; set; }

        /// <summary>
        /// Gets/set the time of the <see cref="Message"/>.
        /// </summary>
        /// <remarks>Make sure Timestamp is specified in UTC; if not it will be assumed local and converted to UTC.</remarks>
        /// <seealso href="https://pushover.net/api#timestamp">Pushover API documentation</seealso>
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// Gets/sets the notification sound for the <see cref="Message"/>.
        /// </summary>
        /// <remarks>A list of available sounds can be retrieved with <see cref="M:NPushover.Pushover.ListSoundsAsync"/>.</remarks>
        /// <seealso cref="Sounds"/>
        /// <seealso cref="M:NPushover.Pushover.ListSoundsAsync"/>
        /// <seealso href="https://pushover.net/api#sounds">Pushover API documentation</seealso>
        public string Sound { get; set; }

        /// <summary>
        /// Get/sets whether the <see cref="Message"/> <see cref="Body"/> is to be interpreted as HTML.
        /// </summary>
        /// <seealso href="https://pushover.net/api#html">Pushover API documentation</seealso>
        /// <remarks>
        /// HTML tags currently supported by Pushover:
        /// <ul>
        ///     <li>&lt;b&gt;word&lt;/b&gt; - display word in bold</li>
        ///     <li>&lt;i&gt;word&lt;/i&gt; - display word in italics</li>
        ///     <li>&lt;u&gt;word&lt;/u&gt; - display word underlined</li>
        ///     <li>&lt;font color="blue"&gt;word&lt;/font&gt; - display word in blue text (most colors and hex codes permitted)</li>
        ///     <li>&lt;a href="http://example.com/"&gt;word&lt;/a&gt; - display word as a tappable link to http://example.com/</li>
        /// </ul>
        /// </remarks>
        public bool IsHtmlBody { get; set; }

        /// <summary>
        /// Gets/set the <see cref="RetryOptions"/> for the <see cref="Message"/>.
        /// </summary>
        /// <seealso href="https://pushover.net/api#priority">Pushover API documentation</seealso>
        public RetryOptions RetryOptions { get; set; }


        public string Attachment { get; set; }


        /// <summary>
        /// Creates a <see cref="Message"/> with the specified <see cref="Sound"/> and default <see cref="Priority"/> (Normal).
        /// </summary>
        /// <param name="sound">The <see cref="Sound"/> of the <see cref="Message"/>.</param>
        /// <returns>A <see cref="Message"/> with the specified <see cref="Sound"/>.</returns>
        public static Message Create(Sounds sound)
        {
            return new Message(sound);
        }

        /// <summary>
        /// Creates a <see cref="Message"/> with the specified <see cref="Body"/> and default <see cref="Priority"/> (Normal) and sound.
        /// </summary>
        /// <param name="body">The <see cref="Message"/> <see cref="Body"/>.</param>
        /// <returns>A <see cref="Message"/> with the specified <see cref="Body"/> and default <see cref="Priority"/> (Normal) and sound.</returns>
        public static Message Create(string body)
        {
            return new Message(body);
        }

        /// <summary>
        /// Creates a <see cref="Message"/> with the specified <see cref="Priority"/> and <see cref="Body"/> and default sound.
        /// </summary>
        /// <param name="priority">The <see cref="Message"/> <see cref="Priority"/>.</param>
        /// <param name="body">The <see cref="Message"/> <see cref="Body"/>.</param>
        /// <returns>A <see cref="Message"/> with the specified <see cref="Priority"/> and <see cref="Body"/> and default sound.</returns>
        public static Message Create(Priority priority, string body)
        {
            return new Message(priority, body);
        }

        /// <summary>
        /// Creates a <see cref="Message"/> with the specified <see cref="Priority"/>, <see cref="Body"/> and <see cref="Sound"/>.
        /// </summary>
        /// <param name="priority">The <see cref="Message"/> <see cref="Priority"/>.</param>
        /// <param name="body">The <see cref="Message"/> <see cref="Body"/>.</param>
        /// <param name="sound">The <see cref="Sound"/> of the <see cref="Message"/>.</param>
        /// <returns>A <see cref="Message"/> with the specified <see cref="Priority"/>, <see cref="Body"/> and <see cref="Sound"/>.</returns>
        public static Message Create(Priority priority, string body, Sounds sound)
        {
            return new Message(priority, body, sound);
        }

        /// <summary>
        /// Creates a <see cref="Message"/> with the specified <see cref="Priority"/>, <see cref="Body"/> and <see cref="Sound"/>.
        /// </summary>
        /// <param name="priority">The <see cref="Message"/> <see cref="Priority"/>.</param>
        /// <param name="body">The <see cref="Message"/> <see cref="Body"/>.</param>
        /// <param name="isHtmlBody">When the <see cref="Body"/> contains HTML, set to true. False otherwise. See <see cref="IsHtmlBody"/>.</param>
        /// <param name="sound">The <see cref="Sound"/> of the <see cref="Message"/>.</param>
        /// <returns>A <see cref="Message"/> with the specified <see cref="Priority"/>, <see cref="Body"/> and <see cref="Sound"/>.</returns>
        public static Message Create(Priority priority, string body, bool isHtmlBody, Sounds sound)
        {
            return new Message(priority, body, isHtmlBody, sound);
        }

        /// <summary>
        /// Creates a <see cref="Message"/> with the specified <see cref="Priority"/>, <see cref="Title"/>, 
        /// <see cref="Body"/> and <see cref="Sound"/>.
        /// </summary>
        /// <param name="priority">The <see cref="Message"/> <see cref="Priority"/>.</param>
        /// <param name="title">The <see cref="Title"/> for the <see cref="Message"/>.</param>
        /// <param name="body">The <see cref="Message"/> <see cref="Body"/>.</param>
        /// <param name="isHtmlBody">When the <see cref="Body"/> contains HTML, set to true. False otherwise. See <see cref="IsHtmlBody"/>.</param>
        /// <param name="sound">The <see cref="Sound"/> of the <see cref="Message"/>.</param>
        /// <returns>
        /// A <see cref="Message"/> with the specified <see cref="Priority"/>, <see cref="Title"/>, <see cref="Body"/> 
        /// and <see cref="Sound"/>.
        /// </returns>
        public static Message Create(Priority priority, string title, string body, bool isHtmlBody, Sounds sound)
        {
            return new Message(priority, title, body, isHtmlBody, sound);
        }

        /// <summary>
        /// Sets the <see cref="Sound"/> property to any of the available <see cref="Sounds"/> values.
        /// </summary>
        /// <param name="sound">The <see cref="Sounds"/> values to set this messages' <see cref="Sound"/> property to.</param>
        /// <seealso cref="Sounds"/>
        /// <seealso href="https://pushover.net/api#sounds">Pushover API documentation</seealso>
        public void SetSound(Sounds sound)
        {
            this.Sound = sound.ToString().ToLowerInvariant();
        }
    }
}
