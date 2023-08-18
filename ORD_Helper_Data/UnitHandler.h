#pragma once
#include "Grade.h"
#include "ProcessInfo.h"
#include "GameState.h"
#include "Resources.h"
#include <fstream>
#include <sstream>
#include <string>
#include <vcclr.h>

namespace ORD_Helper_Data
{
	ref class Grade;

	public ref class UnitHandler
	{
		int _gradeNum;
		cliext::vector<Grade^> _grade;
		std::ofstream *out;
		std::ifstream *in;

		void SaveUnitHandler();
		void SaveGrade();
		void SaveUnit();
		void SaveSubUnitMix(int n, int i);
		void SaveHighestUnitMix(int n, int i);
		void SaveLowestUnitMix(int n, int i);
		void SaveUpperUnitMix(int n, int i);
		template <typename T>
		void Save(T temp);

		void LoadUnitHandler();
		void LoadGrade();
		void LoadUnit();
		void LoadSubUnitMix(int n, int i);
		void LoadHighestUnitMix(int n, int i);
		void LoadLowestUnitMix(int n, int i);
		void LoadUpperUnitMix(int n, int i);
		template <typename T>
		T Load();
		char * Load(int size);


		void DeleteHighestMix(int gradeIndex, int unitIndex, HighestUnitMix ^ highestUnitMix);
		void DeleteLowestMix(int gradeIndex, int unitIndex, Unit ^ unit);
		void SearchSubMix(Unit ^ mainunit, Unit ^ subUnit, int ** tempUnitNum, int ** tempUnitNum_Hero, int num, int num_Hero, int grade, int unit, int * tempGold, int * tempTree, int * tempWispNum, int heroUnitIndex, bool * heroUse);

	public:
		UnitHandler();
		~UnitHandler();

		property int gradeNum {
			int get() { return _gradeNum; }
			void set(int value) {
				if (value > 0)
					_gradeNum = value;
				else
					ErrorMessage(12, "UnitHandler gradeNum ¿À·ù");		//eCode 12
			}
		}

		int wispGradeIndex;
		int selectWispUnitIndex;
		int heroGradeIndex;
		int uniqueGradeIndex;
		Grade ^ grade(int index);

		void ResizeGrade(int size);
		void AddGrade(char * gradeName);
		void RemoveGrade(int gradeIndex);
		void AddUnit(int gradeIndex, char * unitName, array<SubUnitMix^>^subUnitMix, MixResources ^ mixResources, DWORD index);
		void RemoveUnit(int gradeIndex, int unitIndex);
		void AddSubUnitMix(int gradeIndex, int unitIndex, SubUnitMix ^ subUnitMix);
		void RemoveSubUnitMix(int gradeIndex, int unitIndex, int subIndex);

		void Copy(UnitHandler ^ temp);

		void SetLowestMix(int gradeIndex, int unitIndex, Unit ^ unit, int basicNum);
		void SetHighestMix(int gradeIndex, int unitIndex, HighestUnitMix ^ highestUnitMix);
		void SearchUnitNum(ProcessHandler ^ ph, GameStateHandler ^ gh);
		void SearchUnitMix(ResourcesHandler ^ rh);
		void SaveUnitInfo(char * temp);
		void LoadUnitInfo(char * temp);
	};
}