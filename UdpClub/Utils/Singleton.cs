namespace UdpClub.Utils {
    public class Singleton<T> where T : new() {
        private static T _instance;
        private static readonly object ThreadLock = new object();

        public static T Instance {
            get {
                lock (ThreadLock) {
                    if (_instance == null) {
                        _instance = new T();
                    }
                    return _instance;
                }
            }
        }
    }
}