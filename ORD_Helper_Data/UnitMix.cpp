#include "pch.h"
#include "UnitMix.h"

namespace ORD_Helper_Data
{
	SubUnitMix::SubUnitMix()
		:_basicNum(0), _needNum(0), _gradeIndex(-1), _unitIndex(-1), _percent(0), wispPercent(0), heroPercent(0)
	{
		mixResources = gcnew MixResources();
	}

	void SubUnitMix::Copy(SubUnitMix ^ temp)
	{
		this->_basicNum = temp->_basicNum;
		this->needNum = temp->needNum;
		this->gradeIndex = temp->gradeIndex;
		this->unitIndex = temp->unitIndex;
		this->percent = temp->percent;
		this->wispPercent = temp->wispPercent;
		this->heroPercent = temp->heroPercent;
	}

	UpperUnitMix::UpperUnitMix()
		: _gradeIndex(-1), _unitIndex(-1)
	{
	}

	void UpperUnitMix::Copy(UpperUnitMix ^ temp)
	{
		this->gradeIndex = temp->gradeIndex;
		this->unitIndex = temp->unitIndex;
	}

	HighestUnitMix::HighestUnitMix()
		: _gradeIndex(-1), _unitIndex(-1)
	{

	}

	void HighestUnitMix::Copy(HighestUnitMix ^ temp)
	{
		this->gradeIndex = temp->gradeIndex;
		this->unitIndex = temp->unitIndex;
	}

	HighestUnitMix::HighestUnitMix(int _gradeIndex, int _unitIndex)
	{
		this->_gradeIndex = _gradeIndex;
		this->_unitIndex = _unitIndex;
	}

	LowestUnitMix::LowestUnitMix()
		: _basicNum(0), _needNum(0), needWispNum(0), needHeroNum(0), _gradeIndex(-1), _unitIndex(-1)
	{

	}

	void LowestUnitMix::Copy(LowestUnitMix ^ temp)
	{
		this->basicNum = temp->basicNum;
		this->needNum = temp->needNum;
		this->needWispNum = temp->needWispNum;
		this->needHeroNum = temp->needHeroNum;
		this->gradeIndex = temp->gradeIndex;
		this->unitIndex = temp->unitIndex;
	}

	MixResources::MixResources()
		: _basicGold(0), _basicTree(0), _needGold(0), _needTree(0)
	{

	}

	void MixResources::Copy(MixResources ^ temp)
	{
		this->basicGold = temp->basicGold;
		this->needGold = temp->needGold;
		this->basicTree = temp->basicTree;
		this->needTree = temp->needTree;
	}
}