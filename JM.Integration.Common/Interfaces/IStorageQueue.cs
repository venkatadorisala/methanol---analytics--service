namespace JM.Integration.Common.Interfaces
{
    /// <summary>
    ///  Contains methods for queue storage services implementation
    /// </summary>
    public interface IStorageQueue
    {
        /// <summary>
        /// Adds new message into storage queue 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="queueName"></param>
        /// <param name="message"></param>
        /// <returns>Returns new message inserted status</returns>
        bool AddMessageToStorageQueue(string connectionString, string queueName, string message);

    }
}