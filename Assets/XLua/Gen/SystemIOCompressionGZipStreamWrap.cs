#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class SystemIOCompressionGZipStreamWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(System.IO.Compression.GZipStream);
			Utils.BeginObjectRegister(type, L, translator, 0, 9, 6, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Read", _m_Read);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Write", _m_Write);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Flush", _m_Flush);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Seek", _m_Seek);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLength", _m_SetLength);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BeginRead", _m_BeginRead);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BeginWrite", _m_BeginWrite);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EndRead", _m_EndRead);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EndWrite", _m_EndWrite);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "BaseStream", _g_get_BaseStream);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CanRead", _g_get_CanRead);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CanSeek", _g_get_CanSeek);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CanWrite", _g_get_CanWrite);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Length", _g_get_Length);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Position", _g_get_Position);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Position", _s_set_Position);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 3 && translator.Assignable<System.IO.Stream>(L, 2) && translator.Assignable<System.IO.Compression.CompressionMode>(L, 3))
				{
					System.IO.Stream _compressedStream = (System.IO.Stream)translator.GetObject(L, 2, typeof(System.IO.Stream));
					System.IO.Compression.CompressionMode _mode;translator.Get(L, 3, out _mode);
					
					System.IO.Compression.GZipStream gen_ret = new System.IO.Compression.GZipStream(_compressedStream, _mode);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 4 && translator.Assignable<System.IO.Stream>(L, 2) && translator.Assignable<System.IO.Compression.CompressionMode>(L, 3) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4))
				{
					System.IO.Stream _compressedStream = (System.IO.Stream)translator.GetObject(L, 2, typeof(System.IO.Stream));
					System.IO.Compression.CompressionMode _mode;translator.Get(L, 3, out _mode);
					bool _leaveOpen = LuaAPI.lua_toboolean(L, 4);
					
					System.IO.Compression.GZipStream gen_ret = new System.IO.Compression.GZipStream(_compressedStream, _mode, _leaveOpen);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Compression.GZipStream constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Read(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.Compression.GZipStream gen_to_be_invoked = (System.IO.Compression.GZipStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte[] _dest = LuaAPI.lua_tobytes(L, 2);
                    int _dest_offset = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        int gen_ret = gen_to_be_invoked.Read( _dest, _dest_offset, _count );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Write(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.Compression.GZipStream gen_to_be_invoked = (System.IO.Compression.GZipStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte[] _src = LuaAPI.lua_tobytes(L, 2);
                    int _src_offset = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.Write( _src, _src_offset, _count );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Flush(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.Compression.GZipStream gen_to_be_invoked = (System.IO.Compression.GZipStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Flush(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Seek(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.Compression.GZipStream gen_to_be_invoked = (System.IO.Compression.GZipStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    long _offset = LuaAPI.lua_toint64(L, 2);
                    System.IO.SeekOrigin _origin;translator.Get(L, 3, out _origin);
                    
                        long gen_ret = gen_to_be_invoked.Seek( _offset, _origin );
                        LuaAPI.lua_pushint64(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLength(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.Compression.GZipStream gen_to_be_invoked = (System.IO.Compression.GZipStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    long _value = LuaAPI.lua_toint64(L, 2);
                    
                    gen_to_be_invoked.SetLength( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BeginRead(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.Compression.GZipStream gen_to_be_invoked = (System.IO.Compression.GZipStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte[] _buffer = LuaAPI.lua_tobytes(L, 2);
                    int _offset = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    System.AsyncCallback _cback = translator.GetDelegate<System.AsyncCallback>(L, 5);
                    object _state = translator.GetObject(L, 6, typeof(object));
                    
                        System.IAsyncResult gen_ret = gen_to_be_invoked.BeginRead( _buffer, _offset, _count, _cback, _state );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BeginWrite(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.Compression.GZipStream gen_to_be_invoked = (System.IO.Compression.GZipStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte[] _buffer = LuaAPI.lua_tobytes(L, 2);
                    int _offset = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    System.AsyncCallback _cback = translator.GetDelegate<System.AsyncCallback>(L, 5);
                    object _state = translator.GetObject(L, 6, typeof(object));
                    
                        System.IAsyncResult gen_ret = gen_to_be_invoked.BeginWrite( _buffer, _offset, _count, _cback, _state );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EndRead(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.Compression.GZipStream gen_to_be_invoked = (System.IO.Compression.GZipStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.IAsyncResult _async_result = (System.IAsyncResult)translator.GetObject(L, 2, typeof(System.IAsyncResult));
                    
                        int gen_ret = gen_to_be_invoked.EndRead( _async_result );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EndWrite(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.Compression.GZipStream gen_to_be_invoked = (System.IO.Compression.GZipStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.IAsyncResult _async_result = (System.IAsyncResult)translator.GetObject(L, 2, typeof(System.IAsyncResult));
                    
                    gen_to_be_invoked.EndWrite( _async_result );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BaseStream(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.Compression.GZipStream gen_to_be_invoked = (System.IO.Compression.GZipStream)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.BaseStream);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CanRead(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.Compression.GZipStream gen_to_be_invoked = (System.IO.Compression.GZipStream)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.CanRead);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CanSeek(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.Compression.GZipStream gen_to_be_invoked = (System.IO.Compression.GZipStream)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.CanSeek);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CanWrite(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.Compression.GZipStream gen_to_be_invoked = (System.IO.Compression.GZipStream)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.CanWrite);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Length(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.Compression.GZipStream gen_to_be_invoked = (System.IO.Compression.GZipStream)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushint64(L, gen_to_be_invoked.Length);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Position(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.Compression.GZipStream gen_to_be_invoked = (System.IO.Compression.GZipStream)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushint64(L, gen_to_be_invoked.Position);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Position(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.Compression.GZipStream gen_to_be_invoked = (System.IO.Compression.GZipStream)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Position = LuaAPI.lua_toint64(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
