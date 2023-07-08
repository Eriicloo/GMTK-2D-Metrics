using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InstructionType { RUN_RIGHT, RUN_LEFT, JUMP, TRAMPOLINE, COUNT }

public class Instruction : MonoBehaviour
{
    public InstructionType _instructionType;
}
