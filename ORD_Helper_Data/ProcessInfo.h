#pragma once
#include <Windows.h>
#include <tlhelp32.h>
#include <tchar.h>
#include "Error.h"

namespace ORD_Helper_Data
{
	public ref class Process
	{
	private:
		int _processId;
		HANDLE _processH;
		int _processState;
		int _addressLength;
		wchar_t * _address;
	public:
		Process();
		
		property int processId
		{
			int get() { return _processId; }
			void set(int value) {
				if (value >= 0)
					_processId = value;
				else
					ErrorMessage(32, "Process processId set 오류");
			}
		}
		property HANDLE processH
		{
			HANDLE get() { return _processH; }
			void set(HANDLE value) {
				_processH = value;
			}
		}
		property int processState
		{
			int get() { return _processState; }
			void set(int value) {
				if (value >= 0)
					_processState = value;
				else
					ErrorMessage(33, "Process processState set 오류");
			}
		}
		property int addressLength {
			int get() { return _addressLength; }
			void set(int value) {
				if (value > 0)
					_addressLength = value;
				else
					ErrorMessage(57, "Process addressLength set 오류");
			}
		}
		property wchar_t * address {
			wchar_t * get() { return _address; }
			void set(wchar_t * value) {
				if (value != NULL)
				{
					_addressLength = wcslen(value) + 1;
					_address = new wchar_t[_addressLength];
					wcscpy_s(_address, _addressLength, value);
				}
			}
		}
	};


	public ref class ProcessHandler
	{
	private:
		std::ofstream *out;
		std::ifstream *in;

		template <typename T>
		void Save(T temp);
		template <typename T>
		T Load();
		template <typename T>
		T * Load(int size);
	public:
		ProcessHandler();
		~ProcessHandler();
		Process ^ process;

		void Copy(ProcessHandler ^ temp);
		int SetProcessId(const wchar_t * targetProcess);
		char * GetModuleBase(const wchar_t * ModuleName, DWORD procID);
		void CheckProcessInfo();
		void SaveProcess(char * temp);
		void LoadProcess(char * temp);
		void SetAddress(char * temp);
	};
}