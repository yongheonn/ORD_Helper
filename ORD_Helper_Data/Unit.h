#pragma once
#include <cliext/vector>
#include <cstring>
#include <Windows.h>
#include "Error.h"
#include "UnitMix.h"

namespace ORD_Helper_Data
{
	public ref class Unit
	{
	private:
		int _num;
		char * _name;
		int _nameSize;
		char * _mixName;
		int _mixNameSize;
		DWORD _address;
		DWORD _index;
		int _percent;
		int _wispPercent;

		cliext::vector<SubUnitMix^> _subUnitMix;
		int _subNum;
		cliext::vector<UpperUnitMix^>  _upperUnitMix;
		int _upperNum;
		cliext::vector<LowestUnitMix^> _lowestUnitMix;
		int _lowestNum;
		cliext::vector<HighestUnitMix^> _highestUnitMix;
		int _highestNum;

	public:
		Unit();
		
		property int num {
			int get() { return _num; }
			void set(int value) {
				if (value >= 0)
					_num = value;
				else
					ErrorMessage(8, "Unit num set 오류");
			}
		}
		property char * name {
			char * get() { return _name; }
			void set(char * value) {
				if (value != NULL)
				{
					_nameSize = strlen(value) + 1;
					_name = new char[_nameSize];
					strcpy_s(_name, _nameSize, value);
				}
				else
					ErrorMessage(9, "Unit name set 오류");
			}
		}
		property int nameSize {
			int get() { return _nameSize; }
			void set(int value) {
				if (value >= 0)
					_nameSize = value;
				else
					ErrorMessage(14, "Unit num set 오류");
			}
		}
		property char * mixName {
			char * get() { return _mixName; }
			void set(char * value) {
				if (value != NULL)
				{
					_mixNameSize = strlen(value) + 1;
					_mixName = new char[_mixNameSize];
					strcpy_s(_mixName, _mixNameSize, value);
				}
				else
				{
					_mixNameSize = 0;
					_mixName = NULL;
				}
			}
		}
		property int mixNameSize {
			int get() { return _mixNameSize; }
			void set(int value) {
				if (value >= 0)
					_mixNameSize = value;
			}
		}

		property DWORD address {
			DWORD get() { return _address; }
			void set(DWORD value) {
				_address = value;
			}
		}
		property DWORD index {
			DWORD get() { return _index; }
			void set(DWORD value) {
				_index = value;
			}
		}
		property int percent {
			int get() { return _percent; }
			void set(int value) {
				_percent = value;
			}
		}

		property int wispPercent {
			int get() { return _wispPercent; }
			void set(int value) {
				_wispPercent = value;
			}
		}

		int heroPercent;
		
		SubUnitMix ^ subUnitMix(int index);
		property int subNum {
			int get() { return _subNum; }
			void set(int value) {
				if (value >= 0)
					_subNum = value;
				else
					ErrorMessage(28, "Unit subNum set 오류");
			}
		}
		UpperUnitMix ^ upperUnitMix(int index);
		property int upperNum {
			int get() { return _upperNum; }
			void set(int value) {
				if (value >= 0)
					_upperNum = value;
				else
					ErrorMessage(29, "Unit upperNum set 오류");
			}
		}
		LowestUnitMix ^ lowestUnitMix(int index);
		property int lowestNum {
			int get() { return _lowestNum; }
			void set(int value) {
				if (value >= 0)
					_lowestNum = value;
				else
					ErrorMessage(30, "Unit lowestNum set 오류");
			}
		}
		HighestUnitMix ^ highestUnitMix(int index);
		property int highestNum {
			int get() { return _highestNum; }
			void set(int value) {
				if (value >= 0)
					_highestNum = value;
				else
					ErrorMessage(31, "Unit highestNum set 오류");
			}
		}
		int unitIndex;
		MixResources ^mixResources;

		void Copy(Unit ^ temp);
		void ResizeSubUnitMix(int size);
		void ResizeUpperUnitMix(int size);
		void ResizeLowestUnitMix(int size);
		void ResizeHighestUnitMix(int size);
		void RemoveSubUnitMix(int subIndex);
		void RemoveUpperUnitMix(int subIndex);
		void RemoveLowestUnitMix(int subIndex);
		void RemoveHighestUnitMix(int subIndex);
	};
}