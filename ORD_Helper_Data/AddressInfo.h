#pragma once
#include <Windows.h>
#include <cwchar>
#include "Error.h"
#include "UnitHandler.h"
#include "Resources.h"
#include "GameState.h"
#include "ProcessInfo.h"
using namespace System::Runtime::InteropServices;

namespace ORD_Helper_Data
{
	public ref class Address
	{
	private:
		wchar_t * _address;
		int _addressSize;
		cliext::vector<DWORD> _offset;
		int _offsetNum;
	public:
		Address();
		~Address();

		property wchar_t * address
		{
			wchar_t * get() { return _address; }
			void set(wchar_t * value) {
				if (value != NULL)
				{
					_addressSize = wcslen(value) + 1;
					_address = new wchar_t[_addressSize];
					wcscpy_s(_address, _addressSize, value);
				}
			}
		}
		property int addressSize
		{
			int get() { return _addressSize; }
			void set(int value) {
				if (value >= 0)
					_addressSize = value;
			}
		}

		property array<DWORD>^ offset
		{
			array<DWORD>^ get() { 
				array<DWORD>^ temp = gcnew array<DWORD>(_offsetNum);
				for (int n = 0; n < _offsetNum; n++)
					temp[n] = _offset[n];
				return temp; 
			}
			void set(array<DWORD>^ value) {
				for (int n = 0; n < _offsetNum; n++)
					_offset[n] = value[n];
			}
		}

		property int offsetNum
		{
			int get() { return _offsetNum; }
			void set(int value) {
				if (value >= 0)
					_offsetNum = value;
			}
		}

		void ResizeOffset(int size);
		void RemoveOffset(int index);
		void Copy(Address ^ temp);

		void SetOffset(DWORD value, int index);
		DWORD GetOffset(int index);
	};

	public ref class UnitAddress : public Address
	{
	private:
		DWORD _checkAddress;
		DWORD _pointerAddress;

	public:
		UnitAddress();

		property DWORD checkAddress
		{
			DWORD get() { return _checkAddress; }
			void set(DWORD value) {
				_checkAddress = value;
			}
		}
		property DWORD pointerAddress
		{
			DWORD get() { return _pointerAddress; }
			void set(DWORD value) {
				if (value >= 0)
					_pointerAddress = value;
				else
					ErrorMessage(35, "UnitAddress pointerAddress set ¿À·ù");
			}
		}

		bool check;
	};

	public ref class GoldAddress : public Address {};

	public ref class TreeAddress : public Address {};

	public ref class GameStateAddress : public Address {};


	generic <typename classType>
		where classType : Address, gcnew()
	public ref class AddressHandler
	{
	private:
		std::ofstream *out;
		std::ifstream *in;

		void Save(char* temp, int size);

		void Save(INT64 temp, int size);

		generic <typename T>
		T Load();

		wchar_t * Load(int size);

	public:
		AddressHandler();
		~AddressHandler();

		classType addressInfo;
		void SetAddress(char * temp);

		void SaveAddress(char * temp);
		void LoadAddress(char * temp);
	};

	public ref class UnitAddressInfo
	{
	private:
		char * _name;
	public:
		UnitAddressInfo(DWORD address, DWORD index, int num, char * name, int grade, int unit);
		DWORD address;
		DWORD index;
		int num;
		int grade;
		int unit;
		property char * name
		{
			char * get() { return _name; }
			void set(char * value)
			{
				if (value != NULL)
				{
					_name = new char[strlen(value) + 1];
					strcpy_s(_name, strlen(value) + 1, value);
				}
			}
		}
	};

	public ref class UnitAddressHandler : public AddressHandler<UnitAddress^>
	{
		cliext::vector<UnitAddressInfo^> _indexList;
	public:
		UnitAddressHandler();
		int indexListSize;
		UnitAddressInfo ^ indexList(int index);
		void Copy(UnitAddressHandler ^ temp);
		void SearchAddress(UnitHandler ^ uh, ProcessHandler ^ ph);
		void SearchList(UnitHandler ^ uh, ProcessHandler ^ ph);
	};

	public ref class GoldAddressHandler : public AddressHandler<GoldAddress^>
	{
	public:
		void SearchAddress(ResourcesHandler ^ rh, ProcessHandler ^ ph);
	};

	public ref class TreeAddressHandler : public AddressHandler<TreeAddress^>
	{
	public:
		void SearchAddress(ResourcesHandler ^ rh, ProcessHandler ^ ph);
	};

	public ref class ResourcesAddressHandler
	{
	public:
		ResourcesAddressHandler();

		void Copy(ResourcesAddressHandler ^ temp);

		GoldAddressHandler ^ gold;
		TreeAddressHandler ^ tree;
	};

	public ref class GameStateAddressHandler : public AddressHandler<GameStateAddress^>
	{
	public:
		void Copy(GameStateAddressHandler ^ temp);
		void SearchAddress(GameStateHandler ^ gh, ProcessHandler ^ ph);
	};
}