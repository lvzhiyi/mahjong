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
    public class cambriancommonBytesSerializableWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(cambrian.common.BytesSerializable);
			Utils.BeginObjectRegister(type, L, translator, 0, 7, 1, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "instanceData", _m_instanceData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "setDataValue", _m_setDataValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getDataValue", _m_getDataValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "removeKV", _m_removeKV);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getHashTable", _m_getHashTable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "bytesRead", _m_bytesRead);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "bytesWrite", _m_bytesWrite);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "data", _g_get_data);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "data", _s_set_data);
            
			
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
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					cambrian.common.BytesSerializable gen_ret = new cambrian.common.BytesSerializable();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to cambrian.common.BytesSerializable constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_instanceData(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cambrian.common.BytesSerializable gen_to_be_invoked = (cambrian.common.BytesSerializable)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.instanceData(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_setDataValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cambrian.common.BytesSerializable gen_to_be_invoked = (cambrian.common.BytesSerializable)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _key = translator.GetObject(L, 2, typeof(object));
                    object _value = translator.GetObject(L, 3, typeof(object));
                    
                    gen_to_be_invoked.setDataValue( _key, _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getDataValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cambrian.common.BytesSerializable gen_to_be_invoked = (cambrian.common.BytesSerializable)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _key = translator.GetObject(L, 2, typeof(object));
                    
                        object gen_ret = gen_to_be_invoked.getDataValue( _key );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_removeKV(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cambrian.common.BytesSerializable gen_to_be_invoked = (cambrian.common.BytesSerializable)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _key = translator.GetObject(L, 2, typeof(object));
                    
                        bool gen_ret = gen_to_be_invoked.removeKV( _key );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getHashTable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cambrian.common.BytesSerializable gen_to_be_invoked = (cambrian.common.BytesSerializable)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.Collections.Hashtable gen_ret = gen_to_be_invoked.getHashTable(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_bytesRead(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cambrian.common.BytesSerializable gen_to_be_invoked = (cambrian.common.BytesSerializable)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    cambrian.common.ByteBuffer _data = (cambrian.common.ByteBuffer)translator.GetObject(L, 2, typeof(cambrian.common.ByteBuffer));
                    
                    gen_to_be_invoked.bytesRead( _data );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_bytesWrite(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cambrian.common.BytesSerializable gen_to_be_invoked = (cambrian.common.BytesSerializable)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    cambrian.common.ByteBuffer _data = (cambrian.common.ByteBuffer)translator.GetObject(L, 2, typeof(cambrian.common.ByteBuffer));
                    
                    gen_to_be_invoked.bytesWrite( _data );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_data(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cambrian.common.BytesSerializable gen_to_be_invoked = (cambrian.common.BytesSerializable)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.data);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_data(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cambrian.common.BytesSerializable gen_to_be_invoked = (cambrian.common.BytesSerializable)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.data = (System.Collections.Hashtable)translator.GetObject(L, 2, typeof(System.Collections.Hashtable));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
