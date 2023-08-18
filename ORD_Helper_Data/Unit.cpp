#include "pch.h"
#include "Unit.h"

namespace ORD_Helper_Data
{
	Unit::Unit()
		:_num(0), _address(0), _index(0), _percent(0), _wispPercent(0), heroPercent(0)
	{
		_subUnitMix = gcnew cliext::vector<SubUnitMix^>();
		_upperUnitMix = gcnew cliext::vector<UpperUnitMix^>();
		_highestUnitMix = gcnew cliext::vector<HighestUnitMix^>();
		_lowestUnitMix = gcnew cliext::vector<LowestUnitMix^>();
		mixResources = gcnew MixResources();
	}

	UpperUnitMix ^ Unit::upperUnitMix(int index)
	{
		return _upperUnitMix[index];
	}

	HighestUnitMix ^ Unit::highestUnitMix(int index)
	{
		return _highestUnitMix[index];
	}

	LowestUnitMix ^ Unit::lowestUnitMix(int index)
	{
		return _lowestUnitMix[index];
	}

	SubUnitMix ^ Unit::subUnitMix(int index)
	{
		return _subUnitMix[index];
	}

	void Unit::Copy(Unit ^ temp)
	{
		this->name = temp->name;
		this->mixName = temp->mixName;
		this->address = temp->address;
		this->index = temp->index;
		this->num = temp->num;
		this->percent = temp->percent;
		this->wispPercent = temp->wispPercent;
		this->heroPercent = temp->heroPercent;

		this->subNum = temp->subNum;
		ResizeSubUnitMix(this->subNum);
		this->highestNum = temp->highestNum;
		ResizeHighestUnitMix(this->highestNum);
		this->lowestNum = temp->lowestNum;
		ResizeLowestUnitMix(this->lowestNum);
		this->upperNum = temp->upperNum;
		ResizeUpperUnitMix(this->upperNum);
	}

	void Unit::ResizeSubUnitMix(int size)
	{
		int tempSize = _subUnitMix.size();
		_subUnitMix.resize(size);
		subNum = size;
		for (int n = tempSize; n < size; n++)
			_subUnitMix[n] = gcnew SubUnitMix();
	}

	void Unit::ResizeUpperUnitMix(int size)
	{
		int tempSize = _upperUnitMix.size();
		_upperUnitMix.resize(size);
		upperNum = size;
		for (int n = tempSize; n < size; n++)
			_upperUnitMix[n] = gcnew UpperUnitMix();
	}

	void Unit::ResizeLowestUnitMix(int size)
	{
		int tempSize = _lowestUnitMix.size();
		_lowestUnitMix.resize(size);
		lowestNum = size;
		for (int n = tempSize; n < size; n++)
			_lowestUnitMix[n] = gcnew LowestUnitMix();
	}

	void Unit::ResizeHighestUnitMix(int size)
	{
		int tempSize = _highestUnitMix.size();
		_highestUnitMix.resize(size);
		highestNum = size;
		for (int n = tempSize; n < size; n++)
			_highestUnitMix[n] = gcnew HighestUnitMix();
	}

	void Unit::RemoveSubUnitMix(int subIndex)
	{
		_subUnitMix.erase(_subUnitMix.begin() + subIndex);
		subNum--;
	}

	void Unit::RemoveUpperUnitMix(int subIndex)
	{
		_upperUnitMix.erase(_upperUnitMix.begin() + subIndex);
		upperNum--;
	}

	void Unit::RemoveLowestUnitMix(int subIndex)
	{
		_lowestUnitMix.erase(_lowestUnitMix.begin() + subIndex);
		lowestNum--;
	}

	void Unit::RemoveHighestUnitMix(int subIndex)
	{
		_highestUnitMix.erase(_highestUnitMix.begin() + subIndex);
		highestNum--;
	}
}