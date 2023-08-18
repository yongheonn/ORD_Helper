#pragma once
#include <cstring>
#include "Unit.h"

namespace ORD_Helper_Data {
	ref class Unit;
	public ref class Grade
	{
	int _unitNum;
	char * _name;
	int _nameSize;
	cliext::vector<Unit^> _unit;

	public:
		Grade();
		property int unitNum {
			int get(){ return _unitNum; }
			void set(int value) {
				if (value >= 0)
					_unitNum = value;
				else
					ErrorMessage(10, "Grade num set 오류");		//eCode 10
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
					ErrorMessage(11, "Grade name set 오류");		//eCode 11
			}
		}
		property int nameSize {
			int get() { return _nameSize; }
			void set(int value) {
				if (value >= 0)
					_nameSize = value;
				else
					ErrorMessage(10, "Grade nameSize set 오류");		//eCode 13
			}
		}
		Unit ^ unit(int index);
		int gradeIndex;

		void Copy(Grade ^ temp);
		void ResizeUnit(int size);
		void RemoveUnit(int unitIndex);
		/*
		void SetGradeNameSize(char * buffer);
		void SetUnitListNum(char * buffer);
		void AllocateGradeName();
		void SetGradeName(char * buffer);
		void AllocateUnitListInfo();
		*/
	};
}