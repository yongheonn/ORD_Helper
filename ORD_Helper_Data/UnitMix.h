#pragma once
#include "Error.h"

namespace ORD_Helper_Data
{
	ref class MixResources;
	public ref class SubUnitMix
	{
		int _basicNum;
		int _needNum;
		int _gradeIndex;
		int _unitIndex;
		int _percent;

	public:
		SubUnitMix();
		property int basicNum {
			int get() { return _basicNum; }
			void set(int value) {
				if (value >= 0)
					_basicNum = value;
				else
					ErrorMessage(15, "SubUnitMix basicNum set 오류");
			}
		}
		property int needNum {
			int get() { return _needNum; }
			void set(int value) {
				if (value >= 0)
					_needNum = value;
				else
					ErrorMessage(20, "SubUnitMix needNum set 오류");
			}
		}
		property int gradeIndex {
			int get() { return _gradeIndex; }
			void set(int value) {
				if (value >= 0)
					_gradeIndex = value;
				else
					ErrorMessage(16, "SubUnitMix gradeIndex set 오류");
			}
		}
		property int unitIndex {
			int get() { return _unitIndex; }
			void set(int value) {
				if (value >= 0)
					_unitIndex = value;
				else
					ErrorMessage(17, "SubUnitMix unitIndex set 오류");
			}
		}
		property int percent {
			int get() { return _percent; }
			void set(int value) {
				if (value >= 0)
					_percent = value;
				else
					ErrorMessage(27, "SubUnitMix percent set 오류");
			}
		}

		int wispPercent;

		int heroPercent;

		MixResources ^ mixResources;

		void Copy(SubUnitMix ^ temp);
	};

	public ref class UpperUnitMix
	{
		int _gradeIndex;
		int _unitIndex;
	public:
		UpperUnitMix();
		property int gradeIndex {
			int get() { return _gradeIndex; }
			void set(int value) {
				if (value >= 0)
					_gradeIndex = value;
				else
					ErrorMessage(18, "UpperUnitMix gradeIndex set 오류");
			}
		}
		property int unitIndex {
			int get() { return _unitIndex; }
			void set(int value) {
				if (value >= 0)
					_unitIndex = value;
				else
					ErrorMessage(19, "UpperUnitMix unitIndex set 오류");
			}
		}

		void Copy(UpperUnitMix ^ temp);
	};

	public ref class HighestUnitMix
	{
		int _gradeIndex;
		int _unitIndex;
	public:
		HighestUnitMix();
		HighestUnitMix(int _gradeIndex, int _unitIndex);
		property int gradeIndex {
			int get() { return _gradeIndex; }
			void set(int value) {
				if (value >= 0)
					_gradeIndex = value;
				else
					ErrorMessage(21, "HighestUnitMix gradeIndex set 오류");
			}
		}
		property int unitIndex {
			int get() { return _unitIndex; }
			void set(int value) {
				if (value >= 0)
					_unitIndex = value;
				else
					ErrorMessage(22, "HighestUnitMix unitIndex set 오류");
			}
		}

		void Copy(HighestUnitMix ^ temp);
	};

	public ref class LowestUnitMix
	{
		int _basicNum;
		int _needNum;
		int _gradeIndex;
		int _unitIndex;
	public:
		LowestUnitMix();
		property int basicNum {
			int get() { return _basicNum; }
			void set(int value) {
				if (value >= 0)
					_basicNum = value;
				else
					ErrorMessage(23, "LowestUnitMix basicNum set 오류");
			}
		}
		property int needNum {
			int get() { return _needNum; }
			void set(int value) {
				if (value >= 0)
					_needNum = value;
				else
					ErrorMessage(24, "LowestUnitMix needNum set 오류");
			}
		}
		int needWispNum;
		int needHeroNum;
		property int gradeIndex {
			int get() { return _gradeIndex; }
			void set(int value) {
				if (value >= 0)
					_gradeIndex = value;
				else
					ErrorMessage(25, "LowestUnitMix gradeIndex set 오류");
			}
		}
		property int unitIndex {
			int get() { return _unitIndex; }
			void set(int value) {
				if (value >= 0)
					_unitIndex = value;
				else
					ErrorMessage(26, "LowestUnitMix unitIndex set 오류");
			}
		}

		void Copy(LowestUnitMix ^ temp);
	};

	public ref class MixResources
	{
	public:
		int _basicGold;
		int _basicTree;
		int _needGold;
		int _needTree;
	public:
		MixResources();
		property int basicGold {
			int get() { return _basicGold; }
			void set(int value) {
				if (value >= 0)
					_basicGold = value;
				else
					ErrorMessage(53, "MixResources basicGold set 오류");
			}
		}
		property int basicTree {
			int get() { return _basicTree; }
			void set(int value) {
				if (value >= 0)
					_basicTree = value;
				else
					ErrorMessage(54, "MixResources basicTree set 오류");
			}
		}
		property int needGold {
			int get() { return _needGold; }
			void set(int value) {
				if (value >= 0)
					_needGold = value;
				else
					ErrorMessage(55, "MixResources needGold set 오류");
			}
		}
		property int needTree {
			int get() { return _needTree; }
			void set(int value) {
				if (value >= 0)
					_needTree = value;
				else
					ErrorMessage(56, "MixResources needTree set 오류");
			}
		}

		void Copy(MixResources ^ temp);
	};
}