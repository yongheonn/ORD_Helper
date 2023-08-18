#include "pch.h"
#include "Grade.h"

namespace ORD_Helper_Data {
	Grade::Grade()
		:_unitNum(0)
	{
		_unit = gcnew cliext::vector<Unit^>();
	}

	void Grade::Copy(Grade ^ temp)
	{
			this->name = temp->name;
			this->_unitNum = temp->_unitNum;
	}

	Unit ^ Grade::unit(int index)
	{
		return _unit[index];
	}

	void Grade::ResizeUnit(int size)
	{
		int tempSize = _unit.size();

		_unit.resize(size);
		_unitNum = size;
		for (int n = tempSize; n < size; n++)
			_unit[n] = gcnew Unit();

	}

	void Grade::RemoveUnit(int unitIndex)
	{
		_unit.erase(_unit.begin() + unitIndex);
		_unitNum--;
	}
}