#include "pch.h"
#include "UnitHandler.h"

namespace ORD_Helper_Data
{
	UnitHandler::UnitHandler()
	{
		_gradeNum = 0;
		heroGradeIndex = -1;
		wispGradeIndex = -1;
		uniqueGradeIndex = -1;
		selectWispUnitIndex = -1;
		_grade = gcnew cliext::vector<Grade^>();
		out = new std::ofstream();
		in = new std::ifstream();
	}

	UnitHandler::~UnitHandler()
	{
		delete out;
		delete in;
	}

	Grade ^ UnitHandler::grade(int index)
	{
		return _grade[index];
	}

	void UnitHandler::ResizeGrade(int size)
	{
		int tempSize = _grade.size();

		_grade.resize(size);
		gradeNum = size;
		for (int n = tempSize; n < size; n++)
			_grade[n] = gcnew Grade();
	}

	void UnitHandler::AddGrade(char * gradeName)
	{
		_gradeNum++;
		ResizeGrade(_gradeNum);
		_grade[_gradeNum - 1]->name = gradeName;
	}

	void UnitHandler::RemoveGrade(int gradeIndex)
	{
		_grade.erase(_grade.begin() + gradeIndex);
		_gradeNum--;
	}

	void UnitHandler::AddUnit(int gradeIndex, char * unitName, array<SubUnitMix^>^subUnitMix, MixResources ^ mixResources, DWORD index)
	{
		/*unitNum*/
		_grade[gradeIndex]->unitNum++;

		/*unit*/
		_grade[gradeIndex]->ResizeUnit(_grade[gradeIndex]->unitNum);

		int unitIndex = _grade[gradeIndex]->unitNum - 1;

		/*name*/
		_grade[gradeIndex]->unit(unitIndex)->name = unitName;

		if (subUnitMix->Length != NULL)
		{
			_grade[gradeIndex]->unit(unitIndex)->ResizeSubUnitMix(subUnitMix->Length);
			for(int n = 0; n < subUnitMix->Length; n++)
				_grade[gradeIndex]->unit(unitIndex)->subUnitMix(n)->Copy(subUnitMix[n]);

			int * subGradeIndex = new int[_grade[gradeIndex]->unit(unitIndex)->subNum];
			int * subUnitIndex = new int[_grade[gradeIndex]->unit(unitIndex)->subNum];

			for (int n = 0; n < _grade[gradeIndex]->unit(unitIndex)->subNum; n++)
			{
				subGradeIndex[n] = _grade[gradeIndex]->unit(unitIndex)->subUnitMix(n)->gradeIndex;
				subUnitIndex[n] = _grade[gradeIndex]->unit(unitIndex)->subUnitMix(n)->unitIndex;
			}
			
			for (int n = 0; n < _grade[gradeIndex]->unit(unitIndex)->subNum; n++)
			{
				_grade[subGradeIndex[n]]->unit(subUnitIndex[n])->upperNum++;
				_grade[subGradeIndex[n]]->unit(subUnitIndex[n])->ResizeUpperUnitMix(_grade[subGradeIndex[n]]->unit(subUnitIndex[n])->upperNum);
				_grade[subGradeIndex[n]]->unit(subUnitIndex[n])->upperUnitMix(_grade[subGradeIndex[n]]->unit(subUnitIndex[n])->upperNum - 1)->gradeIndex = gradeIndex;
				_grade[subGradeIndex[n]]->unit(subUnitIndex[n])->upperUnitMix(_grade[subGradeIndex[n]]->unit(subUnitIndex[n])->upperNum - 1)->unitIndex = unitIndex;
			}

			HighestUnitMix ^ highestUnitMix = gcnew HighestUnitMix(gradeIndex, unitIndex);
			SetHighestMix(gradeIndex, unitIndex, highestUnitMix);

			SetLowestMix(gradeIndex, unitIndex, _grade[gradeIndex]->unit(unitIndex), 1);
		}
		if(mixResources->basicGold > 0 || mixResources->basicTree > 0)
			_grade[gradeIndex]->unit(unitIndex)->mixResources = mixResources;

		_grade[gradeIndex]->unit(unitIndex)->index = index;
	}

	void UnitHandler::AddSubUnitMix(int gradeIndex, int unitIndex, SubUnitMix ^ subUnitMix)
	{
		_grade[gradeIndex]->unit(unitIndex)->subNum++;
		int subNum_temp = _grade[gradeIndex]->unit(unitIndex)->subNum;
		_grade[gradeIndex]->unit(unitIndex)->ResizeSubUnitMix(subNum_temp);
		_grade[gradeIndex]->unit(unitIndex)->subUnitMix(subNum_temp - 1)->gradeIndex = subUnitMix->gradeIndex;
		_grade[gradeIndex]->unit(unitIndex)->subUnitMix(subNum_temp - 1)->unitIndex = subUnitMix->unitIndex;
		_grade[gradeIndex]->unit(unitIndex)->subUnitMix(subNum_temp - 1)->basicNum = subUnitMix->basicNum;


		_grade[subUnitMix->gradeIndex]->unit(subUnitMix->unitIndex)->upperNum++;
		int upperNum_temp = _grade[subUnitMix->gradeIndex]->unit(subUnitMix->unitIndex)->upperNum;
		_grade[subUnitMix->gradeIndex]->unit(subUnitMix->unitIndex)->ResizeUpperUnitMix(upperNum_temp);
		_grade[subUnitMix->gradeIndex]->unit(subUnitMix->unitIndex)->upperUnitMix(upperNum_temp - 1)->gradeIndex = gradeIndex;
		_grade[subUnitMix->gradeIndex]->unit(subUnitMix->unitIndex)->upperUnitMix(upperNum_temp - 1)->unitIndex = unitIndex;

		HighestUnitMix ^ highestUnitMix = gcnew HighestUnitMix(gradeIndex, unitIndex);
		SetHighestMix(gradeIndex, unitIndex, highestUnitMix);

		for (int n = 0; n < _grade[gradeIndex]->unit(unitIndex)->lowestNum; n++)
			_grade[gradeIndex]->unit(unitIndex)->lowestUnitMix(n)->basicNum = 0;
		SetLowestMix(gradeIndex, unitIndex, _grade[gradeIndex]->unit(unitIndex), 1);
	}

	void UnitHandler::RemoveSubUnitMix(int gradeIndex, int unitIndex, int subIndex)
	{
		HighestUnitMix ^ highestUnitMix = gcnew HighestUnitMix(gradeIndex, unitIndex);
		DeleteHighestMix(gradeIndex, unitIndex, highestUnitMix);
		DeleteLowestMix(gradeIndex, unitIndex, _grade[gradeIndex]->unit(unitIndex));

		int upperGradeIndex = _grade[gradeIndex]->unit(unitIndex)->subUnitMix(subIndex)->gradeIndex;
		int upperUnitIndex = _grade[gradeIndex]->unit(unitIndex)->subUnitMix(subIndex)->unitIndex;

		for (int n = 0; n < _grade[upperGradeIndex]->unit(upperUnitIndex)->upperNum; n++)
		{
			if(_grade[upperGradeIndex]->unit(upperUnitIndex)->upperUnitMix(n)->gradeIndex == gradeIndex && _grade[upperGradeIndex]->unit(upperUnitIndex)->upperUnitMix(n)->unitIndex == unitIndex)
				_grade[upperGradeIndex]->unit(upperUnitIndex)->RemoveUpperUnitMix(n);
		}
		_grade[gradeIndex]->unit(unitIndex)->RemoveSubUnitMix(subIndex);
		
		for (int n = 0; n < _grade[gradeIndex]->unit(unitIndex)->subNum; n++)
		{
			int subGradeIndex = _grade[gradeIndex]->unit(unitIndex)->subUnitMix(n)->gradeIndex;
			int subUnitIndex = _grade[gradeIndex]->unit(unitIndex)->subUnitMix(n)->unitIndex;
			for (int i = 0; i < _grade[subGradeIndex]->unit(subUnitIndex)->upperNum; i++)
			{
				if (_grade[subGradeIndex]->unit(subUnitIndex)->upperUnitMix(i)->gradeIndex == gradeIndex &&
					_grade[subGradeIndex]->unit(subUnitIndex)->upperUnitMix(i)->unitIndex == unitIndex)
				{
					_grade[subGradeIndex]->unit(subUnitIndex)->RemoveUpperUnitMix(i);
				}
			}
		}
	}

	void UnitHandler::SetHighestMix(int gradeIndex, int unitIndex, HighestUnitMix ^ highestUnitMix)
	{
		if (_grade[highestUnitMix->gradeIndex]->unit(highestUnitMix->unitIndex)->upperNum == 0)
		{
			for (int n = 0; n < _grade[gradeIndex]->unit(unitIndex)->subNum; n++)
			{
				bool over = false;		//중복 체크
				int subGrade = _grade[gradeIndex]->unit(unitIndex)->subUnitMix(n)->gradeIndex;
				int subUnit = _grade[gradeIndex]->unit(unitIndex)->subUnitMix(n)->unitIndex;

				for (int i = 0; i < _grade[subGrade]->unit(subUnit)->highestNum; i++)
				{
					if (_grade[subGrade]->unit(subUnit)->highestUnitMix(i)->gradeIndex == highestUnitMix->gradeIndex
						&& _grade[subGrade]->unit(subUnit)->highestUnitMix(i)->unitIndex == highestUnitMix->unitIndex)
						over = true;
				}
				if (over == false)
				{
					_grade[subGrade]->unit(subUnit)->highestNum++;
					_grade[subGrade]->unit(subUnit)->ResizeHighestUnitMix(_grade[subGrade]->unit(subUnit)->highestNum);
					_grade[subGrade]->unit(subUnit)->highestUnitMix(_grade[subGrade]->unit(subUnit)->highestNum - 1)->Copy(highestUnitMix);

					SetHighestMix(subGrade, subUnit, highestUnitMix);
				}
				else
				{
					SetHighestMix(subGrade, subUnit, highestUnitMix);
				}
			}
		}
	}

	void UnitHandler::SetLowestMix(int gradeIndex, int unitIndex, Unit ^ unit, int basicNum)
	{
		if (_grade[gradeIndex]->unit(unitIndex)->subNum > 0)
		{
			for (int n = 0; n < _grade[gradeIndex]->unit(unitIndex)->subNum; n++)
			{
				int subGradeIndex = _grade[gradeIndex]->unit(unitIndex)->subUnitMix(n)->gradeIndex;
				int subUnitIndex = _grade[gradeIndex]->unit(unitIndex)->subUnitMix(n)->unitIndex;

				SetLowestMix(subGradeIndex, subUnitIndex, unit, basicNum * _grade[gradeIndex]->unit(unitIndex)->subUnitMix(n)->basicNum);
			}
		}
		else
		{
			bool check = false;
			for (int n = 0; n < unit->lowestNum; n++)
			{
				if (unit->lowestUnitMix(n)->gradeIndex == gradeIndex && unit->lowestUnitMix(n)->unitIndex == unitIndex)
				{
					unit->lowestUnitMix(n)->basicNum += basicNum;
					check = true;
				}
			}
			if (check == false)
			{
				unit->lowestNum++;
				unit->ResizeLowestUnitMix(unit->lowestNum);
				unit->lowestUnitMix(unit->lowestNum - 1)->gradeIndex = gradeIndex;
				unit->lowestUnitMix(unit->lowestNum - 1)->unitIndex = unitIndex;
				unit->lowestUnitMix(unit->lowestNum - 1)->basicNum = basicNum;
			}
		}
	}

	void UnitHandler::DeleteHighestMix(int gradeIndex, int unitIndex, HighestUnitMix ^ highestUnitMix)
	{
		bool over = false;		//중복 체크
		for (int n = 0; n < _grade[gradeIndex]->unit(unitIndex)->highestNum; n++)
		{
			_grade[gradeIndex]->unit(unitIndex)->RemoveHighestUnitMix(n);
		}

		for (int n = 0; n < _grade[gradeIndex]->unit(unitIndex)->subNum; n++)
		{
			if(_grade[gradeIndex]->unit(unitIndex)->subUnitMix(n)->gradeIndex >= 0)
				DeleteHighestMix(_grade[gradeIndex]->unit(unitIndex)->subUnitMix(n)->gradeIndex,
					_grade[gradeIndex]->unit(unitIndex)->subUnitMix(n)->unitIndex,
					highestUnitMix);
		}
	}

	void UnitHandler::DeleteLowestMix(int gradeIndex, int unitIndex, Unit ^ unit)
	{
		if (_grade[gradeIndex]->unit(unitIndex)->subNum > 0)
		{
			for (int n = 0; n < _grade[gradeIndex]->unit(unitIndex)->subNum; n++)
			{
				int subGradeIndex = _grade[gradeIndex]->unit(unitIndex)->subUnitMix(n)->gradeIndex;
				int subUnitIndex = _grade[gradeIndex]->unit(unitIndex)->subUnitMix(n)->unitIndex;

				DeleteLowestMix(subGradeIndex, subUnitIndex, unit);
			}
		}
		else
		{
			for (int n = 0; n < unit->lowestNum; n++)
			{
				if (unit->lowestUnitMix(n)->gradeIndex == gradeIndex && unit->lowestUnitMix(n)->unitIndex == unitIndex)
				{
					unit->RemoveLowestUnitMix(n);
				}
			}
		}
	}

	void UnitHandler::SearchUnitNum(ProcessHandler ^ ph, GameStateHandler ^ gh)
	{
		
			LPVOID buffer;
			for (int n = 0; n < gradeNum; n++)
			{
				for (int i = 0; i < grade(n)->unitNum; i++)
				{
					if (_grade[n]->unit(i)->address > 0)
					{
						ReadProcessMemory(ph->process->processH, (char*)_grade[n]->unit(i)->address, &buffer, sizeof(int), NULL);
						_grade[n]->unit(i)->num = (int)buffer;
					}
				}
			}
		

	}

	void UnitHandler::SearchSubMix(Unit ^ mainunit, Unit ^ subUnit, int ** tempUnitNum, int ** tempUnitNum_Hero, int num, int num_Hero, int gradeIndex, int unitIndex, int * tempGold, int * tempTree, int * tempWispNum, int heroUnitIndex, bool * heroUse)
	{
		if (subUnit->subNum > 0)
		{
			for (int n = 0; n < subUnit->subNum; n++)
			{
				bool _heroOption = false;
				if (subUnit->subUnitMix(n)->gradeIndex == uniqueGradeIndex && !*heroUse)
				{
					if (strcmp(_grade[subUnit->subUnitMix(n)->gradeIndex]->unit(subUnit->subUnitMix(n)->unitIndex)->name, _grade[heroGradeIndex]->unit(heroUnitIndex)->name) == 0)
					{
						_heroOption = true;
						*heroUse = true;
					}
				}

				if (_heroOption == true)
				{
					if (subUnit->subUnitMix(n)->basicNum * num > tempUnitNum[subUnit->subUnitMix(n)->gradeIndex][subUnit->subUnitMix(n)->unitIndex])
					{
						int need_num = subUnit->subUnitMix(n)->basicNum * num - tempUnitNum[subUnit->subUnitMix(n)->gradeIndex][subUnit->subUnitMix(n)->unitIndex];
						int need_num_hero = subUnit->subUnitMix(n)->basicNum * num - (tempUnitNum[subUnit->subUnitMix(n)->gradeIndex][subUnit->subUnitMix(n)->unitIndex] + 1);

						if (_grade[subUnit->subUnitMix(n)->gradeIndex]->unit(subUnit->subUnitMix(n)->unitIndex)->mixResources->basicGold * need_num > *tempGold)
						{
							mainunit->mixResources->needGold += _grade[subUnit->subUnitMix(n)->gradeIndex]->unit(subUnit->subUnitMix(n)->unitIndex)->mixResources->basicGold * need_num - *tempGold;
							mainunit->mixResources->needTree += _grade[subUnit->subUnitMix(n)->gradeIndex]->unit(subUnit->subUnitMix(n)->unitIndex)->mixResources->basicTree * need_num - *tempTree;

							*tempGold = 0;
							*tempTree = 0;
						}
						else
						{
							*tempGold -= _grade[subUnit->subUnitMix(n)->gradeIndex]->unit(subUnit->subUnitMix(n)->unitIndex)->mixResources->basicGold * need_num;
							*tempTree = _grade[subUnit->subUnitMix(n)->gradeIndex]->unit(subUnit->subUnitMix(n)->unitIndex)->mixResources->basicTree * need_num;
						}

						tempUnitNum[subUnit->subUnitMix(n)->gradeIndex][subUnit->subUnitMix(n)->unitIndex] = 0;
						tempUnitNum_Hero[subUnit->subUnitMix(n)->gradeIndex][subUnit->subUnitMix(n)->unitIndex] = 0;
						SearchSubMix(mainunit, _grade[subUnit->subUnitMix(n)->gradeIndex]->unit(subUnit->subUnitMix(n)->unitIndex),
							tempUnitNum, tempUnitNum_Hero, need_num, need_num_hero, subUnit->subUnitMix(n)->gradeIndex, subUnit->subUnitMix(n)->unitIndex, tempGold, tempTree, tempWispNum, heroUnitIndex, heroUse);
					}
					else
					{
						tempUnitNum[subUnit->subUnitMix(n)->gradeIndex][subUnit->subUnitMix(n)->unitIndex] -= subUnit->subUnitMix(n)->basicNum * num;
						tempUnitNum_Hero[subUnit->subUnitMix(n)->gradeIndex][subUnit->subUnitMix(n)->unitIndex] -= subUnit->subUnitMix(n)->basicNum * num - 1;
					}
				}
				else
				{
					if (subUnit->subUnitMix(n)->basicNum * num > tempUnitNum[subUnit->subUnitMix(n)->gradeIndex][subUnit->subUnitMix(n)->unitIndex])
					{
						int need_num = subUnit->subUnitMix(n)->basicNum * num - tempUnitNum[subUnit->subUnitMix(n)->gradeIndex][subUnit->subUnitMix(n)->unitIndex];
						int need_num_hero = subUnit->subUnitMix(n)->basicNum * num_Hero - tempUnitNum_Hero[subUnit->subUnitMix(n)->gradeIndex][subUnit->subUnitMix(n)->unitIndex];

						if (_grade[subUnit->subUnitMix(n)->gradeIndex]->unit(subUnit->subUnitMix(n)->unitIndex)->mixResources->basicGold * need_num > *tempGold)
						{
							mainunit->mixResources->needGold += _grade[subUnit->subUnitMix(n)->gradeIndex]->unit(subUnit->subUnitMix(n)->unitIndex)->mixResources->basicGold * need_num - *tempGold;
							*tempGold = 0;
						}
						else
						{
							*tempGold -= _grade[subUnit->subUnitMix(n)->gradeIndex]->unit(subUnit->subUnitMix(n)->unitIndex)->mixResources->basicGold * need_num;
						}

						if (_grade[subUnit->subUnitMix(n)->gradeIndex]->unit(subUnit->subUnitMix(n)->unitIndex)->mixResources->basicTree * need_num > * tempTree)
						{
							mainunit->mixResources->needTree += _grade[subUnit->subUnitMix(n)->gradeIndex]->unit(subUnit->subUnitMix(n)->unitIndex)->mixResources->basicTree * need_num - *tempTree;
							*tempTree = 0;
						}
						else
						{
							*tempTree = _grade[subUnit->subUnitMix(n)->gradeIndex]->unit(subUnit->subUnitMix(n)->unitIndex)->mixResources->basicTree * need_num;
						}

						tempUnitNum[subUnit->subUnitMix(n)->gradeIndex][subUnit->subUnitMix(n)->unitIndex] = 0;
						tempUnitNum_Hero[subUnit->subUnitMix(n)->gradeIndex][subUnit->subUnitMix(n)->unitIndex] = 0;
						SearchSubMix(mainunit, _grade[subUnit->subUnitMix(n)->gradeIndex]->unit(subUnit->subUnitMix(n)->unitIndex),
							tempUnitNum, tempUnitNum_Hero, need_num, need_num_hero, subUnit->subUnitMix(n)->gradeIndex, subUnit->subUnitMix(n)->unitIndex, tempGold, tempTree, tempWispNum, heroUnitIndex, heroUse);
					}
					else
					{
						tempUnitNum[subUnit->subUnitMix(n)->gradeIndex][subUnit->subUnitMix(n)->unitIndex] -= subUnit->subUnitMix(n)->basicNum * num;
						tempUnitNum_Hero[subUnit->subUnitMix(n)->gradeIndex][subUnit->subUnitMix(n)->unitIndex] -= subUnit->subUnitMix(n)->basicNum * num;
					}
				}
			}
		}
		else
		{
			for (int n = 0; n < mainunit->lowestNum; n++)
			{
					if (mainunit->lowestUnitMix(n)->gradeIndex == gradeIndex && mainunit->lowestUnitMix(n)->unitIndex == unitIndex)
					{
							if (num > tempUnitNum[gradeIndex][unitIndex])
							{
								mainunit->lowestUnitMix(n)->needNum += num - tempUnitNum[gradeIndex][unitIndex];
								tempUnitNum[gradeIndex][unitIndex] = 0;
							}
							else
								tempUnitNum[gradeIndex][unitIndex] -= num;
							if (num_Hero > tempUnitNum_Hero[gradeIndex][unitIndex])
							{
								mainunit->lowestUnitMix(n)->needHeroNum += num_Hero - tempUnitNum_Hero[gradeIndex][unitIndex];
								if (num_Hero - tempUnitNum_Hero[gradeIndex][unitIndex] > *tempWispNum)
								{
									mainunit->lowestUnitMix(n)->needWispNum += num_Hero - (tempUnitNum_Hero[gradeIndex][unitIndex] + *tempWispNum);

									*tempWispNum = 0;
								}
								else
									*tempWispNum -= num_Hero - tempUnitNum_Hero[gradeIndex][unitIndex];
							}
							else
								tempUnitNum_Hero[gradeIndex][unitIndex] -= num_Hero;
					}
			}
		}
	}

	void UnitHandler::SearchUnitMix(ResourcesHandler ^ rh)
	{
		int ** tempUnitNum = new int *[gradeNum];
		int ** tempUnitNum_Hero = new int *[gradeNum];
		int tempWispNum = 0;
		if(wispGradeIndex >= 0 && selectWispUnitIndex >= 0)
			tempWispNum = _grade[wispGradeIndex]->unit(selectWispUnitIndex)->num;
		int heroUnitIndex = -1;
		bool heroUse = true;
		int tempGold = rh->resources->gold;
		int tempTree = rh->resources->tree;

		float percent = 1;
		float wispPercent = 1;
		float heroPercent = 1;
		int subBasicNum = 0;
		int subNeedNum = 0;
		int subNeedWispNum = 0;
		int subHeroNum = 0;
		int basicNum = 0;
		int needNum = 0;
		int needWispNum = 0;
		int heroNum = 0;
		int subNeedGold = 0;
		int subBasicGold = 0;
		int subNeedTree = 0;
		int subBasicTree = 0;
		int needGold = 0;
		int basicGold = 0;
		int needTree = 0;
		int basicTree = 0;
		for (int n = 0; n < gradeNum; n++)
		{
			tempUnitNum[n] = new int[_grade[n]->unitNum];
			tempUnitNum_Hero[n] = new int[_grade[n]->unitNum];
			for (int i = 0; i < _grade[n]->unitNum; i++)
			{
				tempUnitNum[n][i] = _grade[n]->unit(i)->num;
				tempUnitNum_Hero[n][i] = _grade[n]->unit(i)->num;
			}
		}

		if (heroGradeIndex >= 0)
		{
			for (int n = 0; n < _grade[heroGradeIndex]->unitNum; n++)
			{
				if (_grade[heroGradeIndex]->unit(n)->num > 0)
				{
					heroUnitIndex = n;
					heroUse = false;
				}
			}
		}

		for (int n = 0; n < gradeNum; n++)
		{
			for (int i = 0; i < _grade[n]->unitNum; i++)
			{
				if (_grade[n]->unit(i)->subNum > 0)
				{
					_grade[n]->unit(i)->mixResources->needGold = 0;
					_grade[n]->unit(i)->mixResources->needTree = 0;
					
					for (int j = 0; j < _grade[n]->unit(i)->subNum; j++)
					{
						int subGrade = _grade[n]->unit(i)->subUnitMix(j)->gradeIndex;
						int subUnit = _grade[n]->unit(i)->subUnitMix(j)->unitIndex;

						_grade[subGrade]->unit(subUnit)->mixResources->needGold = 0;
						_grade[subGrade]->unit(subUnit)->mixResources->needTree = 0;

							for (int k = 0; k < _grade[subGrade]->unit(subUnit)->lowestNum; k++)
							{
								_grade[subGrade]->unit(subUnit)->lowestUnitMix(k)->needNum = 0;
								_grade[subGrade]->unit(subUnit)->lowestUnitMix(k)->needWispNum = 0;
								_grade[subGrade]->unit(subUnit)->lowestUnitMix(k)->needHeroNum = 0;
							}
							if (_grade[subGrade]->unit(subUnit)->subNum > 0)
							{
								bool heroOption = false;
								if (subGrade == uniqueGradeIndex && heroUnitIndex >= 0 && !heroUse)
								{
									if (strcmp(_grade[subGrade]->unit(subUnit)->name, _grade[heroGradeIndex]->unit(heroUnitIndex)->name) == 0)
									{
										heroOption = true;
										heroUse = true;
									}
								}

								if (heroOption == false)
								{
									if (_grade[n]->unit(i)->subUnitMix(j)->basicNum > tempUnitNum[subGrade][subUnit])
									{
										_grade[n]->unit(i)->subUnitMix(j)->needNum = _grade[n]->unit(i)->subUnitMix(j)->basicNum - tempUnitNum[subGrade][subUnit];
										tempUnitNum[subGrade][subUnit] = 0;
										tempUnitNum_Hero[subGrade][subUnit] = 0;
										SearchSubMix(_grade[subGrade]->unit(subUnit), _grade[subGrade]->unit(subUnit), tempUnitNum, tempUnitNum_Hero,
											_grade[n]->unit(i)->subUnitMix(j)->needNum, _grade[n]->unit(i)->subUnitMix(j)->needNum, subGrade, subUnit, &tempGold, &tempTree, &tempWispNum, heroUnitIndex, &heroUse);
									}
									else
									{
										_grade[n]->unit(i)->subUnitMix(j)->needNum = 0;
										tempUnitNum[subGrade][subUnit] -= _grade[n]->unit(i)->subUnitMix(j)->basicNum;
										tempUnitNum_Hero[subGrade][subUnit] -= _grade[n]->unit(i)->subUnitMix(j)->basicNum;
									}
								}
								else
								{
									if (_grade[n]->unit(i)->subUnitMix(j)->basicNum > tempUnitNum[subGrade][subUnit])
									{
										_grade[n]->unit(i)->subUnitMix(j)->needNum = _grade[n]->unit(i)->subUnitMix(j)->basicNum - tempUnitNum[subGrade][subUnit];
										tempUnitNum[subGrade][subUnit] = 0;
										tempUnitNum_Hero[subGrade][subUnit] = 0;
										SearchSubMix(_grade[subGrade]->unit(subUnit), _grade[subGrade]->unit(subUnit), tempUnitNum, tempUnitNum_Hero,
											_grade[n]->unit(i)->subUnitMix(j)->needNum, _grade[n]->unit(i)->subUnitMix(j)->needNum - 1, subGrade, subUnit, &tempGold, &tempTree, &tempWispNum, heroUnitIndex, &heroUse);
									}
									else
									{
										_grade[n]->unit(i)->subUnitMix(j)->needNum = 0;
										tempUnitNum[subGrade][subUnit] -= _grade[n]->unit(i)->subUnitMix(j)->basicNum;
										tempUnitNum_Hero[subGrade][subUnit] -= (_grade[n]->unit(i)->subUnitMix(j)->basicNum - 1);
									}
								}
								for (int k = 0; k < _grade[subGrade]->unit(subUnit)->lowestNum; k++)
								{
									subBasicNum += _grade[subGrade]->unit(subUnit)->lowestUnitMix(k)->basicNum;
									subNeedNum += _grade[subGrade]->unit(subUnit)->lowestUnitMix(k)->needNum;
									subNeedWispNum += _grade[subGrade]->unit(subUnit)->lowestUnitMix(k)->needWispNum;
									subHeroNum += _grade[subGrade]->unit(subUnit)->lowestUnitMix(k)->needHeroNum;
								}
							}
							else
							{
								subBasicNum = _grade[n]->unit(i)->subUnitMix(j)->basicNum;
								if (_grade[n]->unit(i)->subUnitMix(j)->basicNum > tempUnitNum[subGrade][subUnit])
								{
									_grade[n]->unit(i)->subUnitMix(j)->needNum = _grade[n]->unit(i)->subUnitMix(j)->basicNum - tempUnitNum[subGrade][subUnit];

									subNeedNum += _grade[n]->unit(i)->subUnitMix(j)->basicNum - tempUnitNum[subGrade][subUnit];
									subHeroNum += _grade[n]->unit(i)->subUnitMix(j)->basicNum - tempUnitNum[subGrade][subUnit];
									if (_grade[n]->unit(i)->subUnitMix(j)->basicNum - tempUnitNum[subGrade][subUnit] > tempWispNum)
									{
										subNeedWispNum += _grade[n]->unit(i)->subUnitMix(j)->basicNum - (tempUnitNum[subGrade][subUnit] + tempWispNum);
										tempWispNum = 0;
									}
									else
										tempWispNum -= _grade[n]->unit(i)->subUnitMix(j)->basicNum - tempUnitNum[subGrade][subUnit];
									tempUnitNum[subGrade][subUnit] = 0;
									tempUnitNum_Hero[subGrade][subUnit] = 0;
								}
								else
								{
									_grade[n]->unit(i)->subUnitMix(j)->needNum = 0;
									tempUnitNum[subGrade][subUnit] -= _grade[n]->unit(i)->subUnitMix(j)->basicNum;
									tempUnitNum_Hero[subGrade][subUnit] -= _grade[n]->unit(i)->subUnitMix(j)->basicNum;
								}
								if (_grade[subGrade]->unit(subUnit)->mixResources->basicGold > tempGold)
								{
									_grade[n]->unit(i)->subUnitMix(j)->mixResources->needGold += _grade[subGrade]->unit(subUnit)->mixResources->basicGold - tempGold;
									tempGold = 0;
								}
								else
									tempGold -= _grade[subGrade]->unit(subUnit)->mixResources->basicGold;
								if (_grade[subGrade]->unit(subUnit)->mixResources->basicTree > tempTree)
								{
									_grade[n]->unit(i)->subUnitMix(j)->mixResources->needTree += _grade[subGrade]->unit(subUnit)->mixResources->basicTree - tempTree;
									tempTree = 0;
								}
								else
									tempTree -= _grade[subGrade]->unit(subUnit)->mixResources->basicTree;
							}

							subBasicGold = _grade[subGrade]->unit(subUnit)->mixResources->basicGold;
							subBasicTree = _grade[subGrade]->unit(subUnit)->mixResources->basicTree;
							subNeedGold = _grade[n]->unit(i)->subUnitMix(j)->mixResources->needGold;
							subNeedTree = _grade[n]->unit(i)->subUnitMix(j)->mixResources->needTree;

							if (subBasicNum != 0)
							{
								percent -= ((float)(subNeedNum + subNeedGold + subNeedTree) / (float)(subBasicNum + subBasicGold + subBasicTree)) / _grade[n]->unit(i)->subUnitMix(j)->basicNum;
								wispPercent -= ((float)(subNeedWispNum + subNeedGold + subNeedTree) / (float)(subBasicNum + subBasicGold + subBasicTree)) / _grade[n]->unit(i)->subUnitMix(j)->basicNum;
								heroPercent -= ((float)(subHeroNum + subNeedGold + subNeedTree) / (float)(subBasicNum + subBasicGold + subBasicTree)) / _grade[n]->unit(i)->subUnitMix(j)->basicNum;
								_grade[n]->unit(i)->subUnitMix(j)->percent = percent * 100;
								_grade[n]->unit(i)->subUnitMix(j)->wispPercent = wispPercent * 100;
								_grade[n]->unit(i)->subUnitMix(j)->heroPercent = heroPercent * 100;
							}

							needNum += subNeedNum;
							needWispNum += subNeedWispNum;
							heroNum += subHeroNum;
							needGold += subNeedGold;
							needTree += subNeedTree;
							basicGold += subBasicGold;
							basicTree += subBasicTree;
							percent = 1;
							wispPercent = 1;
							heroPercent = 1;
							subNeedNum = 0;
							subNeedWispNum = 0;
							subHeroNum = 0;
							subBasicNum = 0;
							subBasicGold = 0;
							subBasicTree = 0;
							subNeedGold = 0;
							subNeedTree = 0;
					}
					
					for (int j = 0; j < _grade[n]->unit(i)->lowestNum; j++)
						basicNum += _grade[n]->unit(i)->lowestUnitMix(j)->basicNum;

					basicGold += _grade[n]->unit(i)->mixResources->basicGold;
					basicTree += _grade[n]->unit(i)->mixResources->basicTree;

					if (_grade[n]->unit(i)->mixResources->basicGold > tempGold)
						needGold += _grade[n]->unit(i)->mixResources->basicGold - tempGold;
					if (_grade[n]->unit(i)->mixResources->basicTree > tempTree)
						needTree += _grade[n]->unit(i)->mixResources->basicTree - tempTree;

					_grade[n]->unit(i)->mixResources->needGold = needGold;
					_grade[n]->unit(i)->mixResources->needTree = needTree;

					if (basicNum != 0)
					{
						percent -= (float)(needNum + (needGold / 10000) + needTree) / (float)(basicNum + (basicGold / 10000) + basicTree);
						wispPercent -= (float)(needWispNum + (needGold / 10000) + needTree) / (float)(basicNum + (basicGold / 10000) + basicTree);
						heroPercent -= (float)(heroNum + (needGold / 10000) + needTree) / (float)(basicNum + (basicGold / 10000) + basicTree);
						_grade[n]->unit(i)->percent = percent * 100;
						_grade[n]->unit(i)->wispPercent = wispPercent * 100;
						_grade[n]->unit(i)->heroPercent = heroPercent * 100;
					}
					needNum = 0;
					needWispNum = 0;
					heroNum = 0;
					basicNum = 0;
					percent = 1;
					wispPercent = 1;
					heroPercent = 1;
					
					if (heroUnitIndex >= 0)
						heroUse = false;

					for (int a = 0; a < gradeNum; a++)
					{
						for (int b = 0; b < _grade[a]->unitNum; b++)
						{
							tempUnitNum[a][b] = _grade[a]->unit(b)->num;
							tempUnitNum_Hero[a][b] = _grade[a]->unit(b)->num;
						}
					}

					if (wispGradeIndex >= 0 && selectWispUnitIndex >= 0)
						tempWispNum = _grade[wispGradeIndex]->unit(selectWispUnitIndex)->num;

					basicGold = 0;
					basicTree = 0;
					needGold = 0;
					needTree = 0;
					tempGold = rh->resources->gold;
					tempTree = rh->resources->tree;
				}
				else
				{
						if (_grade[n]->unit(i)->num > 0)
							_grade[n]->unit(i)->percent = 100;
						else
							_grade[n]->unit(i)->percent = 0;
				}
			}
		}
		for (int n = 0; n < gradeNum; n++)
		{
			delete[]tempUnitNum[n];
			delete[]tempUnitNum_Hero[n];
		}
		delete[]tempUnitNum;
		delete[]tempUnitNum_Hero;
	}

	void UnitHandler::Copy(UnitHandler ^ temp)
	{
		this->gradeNum = temp->_gradeNum;
		this->wispGradeIndex = temp->wispGradeIndex;
		this->heroGradeIndex = temp->heroGradeIndex;
		this->uniqueGradeIndex = temp->uniqueGradeIndex;
		this->selectWispUnitIndex = temp->selectWispUnitIndex;

		this->ResizeGrade(this->_gradeNum);
		for (int n = 0; n < this->_gradeNum; n++)
		{
				this->_grade[n]->Copy(temp->_grade[n]);

				this->_grade[n]->ResizeUnit(this->_grade[n]->unitNum);

				for (int i = 0; i < this->_grade[n]->unitNum; i++)
				{
					this->_grade[n]->unit(i)->Copy(temp->_grade[n]->unit(i));
					for (int j = 0; j < this->_grade[n]->unit(i)->subNum; j++)
					{
						this->_grade[n]->unit(i)->subUnitMix(j)->Copy(temp->_grade[n]->unit(i)->subUnitMix(j));
						this->_grade[n]->unit(i)->subUnitMix(j)->mixResources->Copy(temp->_grade[n]->unit(i)->subUnitMix(j)->mixResources);
					}
					for (int j = 0; j < this->_grade[n]->unit(i)->upperNum; j++)
						this->_grade[n]->unit(i)->upperUnitMix(j)->Copy(temp->_grade[n]->unit(i)->upperUnitMix(j));
					for (int j = 0; j < this->_grade[n]->unit(i)->highestNum; j++)
						this->_grade[n]->unit(i)->highestUnitMix(j)->Copy(temp->_grade[n]->unit(i)->highestUnitMix(j));
					for (int j = 0; j < this->_grade[n]->unit(i)->lowestNum; j++)
						this->_grade[n]->unit(i)->lowestUnitMix(j)->Copy(temp->_grade[n]->unit(i)->lowestUnitMix(j));

					this->_grade[n]->unit(i)->mixResources->Copy(temp->_grade[n]->unit(i)->mixResources);
				}
		}
	}

	void UnitHandler::RemoveUnit(int gradeIndex, int unitIndex)
	{
		_grade[gradeIndex]->RemoveUnit(unitIndex);
	}

	void UnitHandler::SaveUnitHandler()
	{
		Save(_gradeNum);
		Save(wispGradeIndex);
		Save(selectWispUnitIndex);
		Save(heroGradeIndex);
		Save(uniqueGradeIndex);
	}

	void UnitHandler::SaveGrade()
	{
		for (int n = 0; n < _gradeNum; n++)
		{
			Save(_grade[n]->unitNum);
			Save(_grade[n]->nameSize);
			Save(_grade[n]->name);
		}
	}

	void UnitHandler::SaveUnit()
	{
		for (int n = 0; n < _gradeNum; n++)
		{
			for (int i = 0; i < _grade[n]->unitNum; i++)
			{
				Save(_grade[n]->unit(i)->nameSize);
				Save(_grade[n]->unit(i)->name);
				Save(_grade[n]->unit(i)->mixNameSize);
				Save(_grade[n]->unit(i)->mixName);
				Save(_grade[n]->unit(i)->index);
				Save(_grade[n]->unit(i)->subNum);
				SaveSubUnitMix(n, i);
				Save(_grade[n]->unit(i)->highestNum);
				SaveHighestUnitMix(n, i);
				Save(_grade[n]->unit(i)->lowestNum);
				SaveLowestUnitMix(n, i);
				Save(_grade[n]->unit(i)->upperNum);
				SaveUpperUnitMix(n, i);
				Save(_grade[n]->unit(i)->mixResources->basicGold);
				Save(_grade[n]->unit(i)->mixResources->basicTree);
			}
		}
	}

	void UnitHandler::SaveSubUnitMix(int n, int i)
	{
		for (int j = 0; j < _grade[n]->unit(i)->subNum; j++)
		{
			Save(_grade[n]->unit(i)->subUnitMix(j)->basicNum);
			Save(_grade[n]->unit(i)->subUnitMix(j)->gradeIndex);
			Save(_grade[n]->unit(i)->subUnitMix(j)->unitIndex);
			Save(_grade[n]->unit(i)->subUnitMix(j)->mixResources->basicGold);
			Save(_grade[n]->unit(i)->subUnitMix(j)->mixResources->basicTree);
		}
	}
	void UnitHandler::SaveHighestUnitMix(int n, int i)
	{
		for (int j = 0; j < _grade[n]->unit(i)->highestNum; j++)
		{
			Save(_grade[n]->unit(i)->highestUnitMix(j)->gradeIndex);
			Save(_grade[n]->unit(i)->highestUnitMix(j)->unitIndex);
		}
	}
	void UnitHandler::SaveLowestUnitMix(int n, int i)
	{
		for (int j = 0; j < _grade[n]->unit(i)->lowestNum; j++)
		{
			Save(_grade[n]->unit(i)->lowestUnitMix(j)->basicNum);
			Save(_grade[n]->unit(i)->lowestUnitMix(j)->gradeIndex);
			Save(_grade[n]->unit(i)->lowestUnitMix(j)->unitIndex);
		}
	}
	void UnitHandler::SaveUpperUnitMix(int n, int i)
	{
		for (int j = 0; j < _grade[n]->unit(i)->upperNum; j++)
		{
			Save(_grade[n]->unit(i)->upperUnitMix(j)->gradeIndex);
			Save(_grade[n]->unit(i)->upperUnitMix(j)->unitIndex);
		}
	}

	template <typename T>
	void UnitHandler::Save(T temp)
	{
		if (std::is_same<T, char*>::value)
		{
			if(temp != NULL)
				out->write((const char*)temp, strlen((char*)temp) + 1);
		}
		else
			out->write((const char*)&temp, sizeof(T));
	}

	void UnitHandler::LoadUnitHandler()
	{
		_gradeNum = Load<int>();
		wispGradeIndex = Load<int>();
		selectWispUnitIndex = Load<int>();
		heroGradeIndex = Load<int>();
		uniqueGradeIndex = Load<int>();
	}

	void UnitHandler::LoadGrade()
	{
		ResizeGrade(_gradeNum);
		for (int n = 0; n < _gradeNum; n++)
		{
			_grade[n]->unitNum = Load<int>();
			_grade[n]->nameSize = Load<int>();
			_grade[n]->name = Load(_grade[n]->nameSize);
		}
	}

	void UnitHandler::LoadUnit()
	{
		for (int n = 0; n < _gradeNum; n++)
		{
			_grade[n]->ResizeUnit(_grade[n]->unitNum);
			for (int i = 0; i < _grade[n]->unitNum; i++)
			{
				_grade[n]->unit(i)->nameSize = Load<int>();
				_grade[n]->unit(i)->name = Load(_grade[n]->unit(i)->nameSize);
				_grade[n]->unit(i)->mixNameSize = Load<int>();
				_grade[n]->unit(i)->mixName = Load(_grade[n]->unit(i)->mixNameSize);
				_grade[n]->unit(i)->index = Load<DWORD>();
				_grade[n]->unit(i)->subNum = Load<int>();
				_grade[n]->unit(i)->ResizeSubUnitMix(_grade[n]->unit(i)->subNum);
				LoadSubUnitMix(n, i);
				_grade[n]->unit(i)->highestNum = Load<int>();
				_grade[n]->unit(i)->ResizeHighestUnitMix(_grade[n]->unit(i)->highestNum);
				LoadHighestUnitMix(n, i);
				_grade[n]->unit(i)->lowestNum = Load<int>();
				_grade[n]->unit(i)->ResizeLowestUnitMix(_grade[n]->unit(i)->lowestNum);
				LoadLowestUnitMix(n, i);
				_grade[n]->unit(i)->upperNum = Load<int>();
				_grade[n]->unit(i)->ResizeUpperUnitMix(_grade[n]->unit(i)->upperNum);
				LoadUpperUnitMix(n, i);
				_grade[n]->unit(i)->mixResources->basicGold = Load<int>();
				_grade[n]->unit(i)->mixResources->basicTree = Load<int>();
			}
		}
	}

	void UnitHandler::LoadSubUnitMix(int n, int i)
	{
		for (int j = 0; j < _grade[n]->unit(i)->subNum; j++)
		{
			_grade[n]->unit(i)->subUnitMix(j)->basicNum = Load<int>();
			_grade[n]->unit(i)->subUnitMix(j)->gradeIndex= Load<int>();
			_grade[n]->unit(i)->subUnitMix(j)->unitIndex = Load<int>();
			_grade[n]->unit(i)->subUnitMix(j)->mixResources->basicGold = Load<int>();
			_grade[n]->unit(i)->subUnitMix(j)->mixResources->basicTree = Load<int>();
		}
	}
	void UnitHandler::LoadHighestUnitMix(int n, int i)
	{
		for (int j = 0; j < _grade[n]->unit(i)->highestNum; j++)
		{
			_grade[n]->unit(i)->highestUnitMix(j)->gradeIndex = Load<int>();
			_grade[n]->unit(i)->highestUnitMix(j)->unitIndex = Load<int>();
		}
	}
	void UnitHandler::LoadLowestUnitMix(int n, int i)
	{
		for (int j = 0; j < _grade[n]->unit(i)->lowestNum; j++)
		{
			_grade[n]->unit(i)->lowestUnitMix(j)->basicNum = Load<int>();
			_grade[n]->unit(i)->lowestUnitMix(j)->gradeIndex = Load<int>();
			_grade[n]->unit(i)->lowestUnitMix(j)->unitIndex = Load<int>();
		}
	}
	void UnitHandler::LoadUpperUnitMix(int n, int i)
	{
		for (int j = 0; j < _grade[n]->unit(i)->upperNum; j++)
		{
			_grade[n]->unit(i)->upperUnitMix(j)->gradeIndex = Load<int>();
			_grade[n]->unit(i)->upperUnitMix(j)->unitIndex = Load<int>();
		}
	}

	template <typename T>
	T UnitHandler::Load()
	{
		T temp;
		in->read((char*)&temp, sizeof(T));
		return temp;
	}

	char * UnitHandler::Load(int size)
	{
		if (size > 0)
		{
			char * temp = new char[size];
			in->read((char*)temp, size);
			return temp;
		}
		else
			return NULL;
	}

	void UnitHandler::SaveUnitInfo(char * temp)
	{
		char * directory = new char[strlen("Version/") + strlen(temp) + strlen("/ORD_Helper_Data.Unit.bin") + 1];
		strcpy_s(directory, strlen("Version/") + 1, "Version/");
		strcat_s(directory, strlen("Version/") + strlen(temp) + 1, temp);
		strcat_s(directory, strlen("Version/") + strlen(temp) + strlen("/ORD_Helper_Data.Unit.bin") + 1, "/ORD_Helper_Data.Unit.bin");
		out->open(directory, std::ios::binary);
		if (out->is_open())
		{
			SaveUnitHandler();
			SaveGrade();
			SaveUnit();
			out->close();
		}
	}
	void UnitHandler::LoadUnitInfo(char * temp)
	{
		char * directory = new char[strlen("Version/") + strlen(temp) + strlen("/ORD_Helper_Data.Unit.bin") + 1];
		strcpy_s(directory, strlen("Version/") + 1, "Version/");
		strcat_s(directory, strlen("Version/") + strlen(temp) + 1, temp);
		strcat_s(directory, strlen("Version/") + strlen(temp) + strlen("/ORD_Helper_Data.Unit.bin") + 1, "/ORD_Helper_Data.Unit.bin");
		in->open(directory, std::ios::binary);

		if (in->is_open())
		{
			LoadUnitHandler();
			LoadGrade();
			LoadUnit();
			in->close();
		}
	}
}