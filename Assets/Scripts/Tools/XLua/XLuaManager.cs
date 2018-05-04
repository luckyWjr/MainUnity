using XLua;

namespace Tool {

	public class XLuaManager {

        LuaEnv m_luaEnv;
        static XLuaManager m_instance;

        public static XLuaManager instance {
            get {
                if(m_instance == null) {
                    m_instance = new XLuaManager();
                }
                return m_instance;
            }
        }

        XLuaManager() {
            if(m_luaEnv == null) {
                m_luaEnv = new LuaEnv();
            }
        }

        public void Start() {
            m_luaEnv.DoString("require 'main'");
        }

        public void Dispose() {
            if(m_luaEnv != null) {
                m_luaEnv.Dispose();
            }
        }

        public void Tick() {
            if(m_luaEnv != null) {
                m_luaEnv.Tick();
            }
        }
    }
}