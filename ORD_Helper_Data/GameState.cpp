#include "pch.h"
#include "GameState.h"

namespace ORD_Helper_Data
{
	GameState::GameState()
	{ }

	GameStateHandler::GameStateHandler()
	{
		gameState = gcnew GameState();
	}

	void GameStateHandler::Copy(GameStateHandler ^ temp)
	{
		this->gameState->state = temp->gameState->state;
		this->gameState->address = temp->gameState->address;
	}

	void GameStateHandler::ResearchGameState(ProcessHandler ^ ph)
	{
		DWORD value;
		ReadProcessMemory(ph->process->processH, (char *)gameState->address, &value, sizeof(DWORD), NULL);
		gameState->state = value;
	}
}