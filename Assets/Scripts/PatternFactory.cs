using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PatternFactory
{
    public static void CreateAttackPattern(ref int attackKind, out List<AttackBase> attackPatterns)
    {
        eAttackPattern attackPattern = (eAttackPattern)attackKind;
        attackPatterns = new List<AttackBase>();
        switch (attackPattern)
        {
            case eAttackPattern.NO_ATTACK:
                attackPatterns.Add(null);
                break;
            case eAttackPattern.ATTACK_WITH_EIGHT:
                attackPatterns.Add(new Attack_EightDirection());
                break;
            case eAttackPattern.ATTACK_PLAYER_DIRECTION_1:
                attackPatterns.Add(new AttackWithDirection());
                break;
            case eAttackPattern.ATTACK_PLAYER_DIRECTION_2:
                attackPatterns.Add(new AttackWithDirection());
                break;
            case eAttackPattern.ATTACK_HOMING:
                attackPatterns.Add(new Homing());
                break;
            case eAttackPattern.ATTACK_WITH_EIGHT_AND_PLAYER_DIRECTION:
                attackPatterns.Add(new Attack_EightDirection());
                attackPatterns.Add(new AttackWithDirection());
                break;
        }

    }

    public static void CreateMovePattern(ref int moveKind, out IMovePattern normalMovePattern, out IMovePattern findMovePattern, Transform transform)
    {
        eMovePattern movePattern = (eMovePattern)moveKind;
        normalMovePattern = null;
        findMovePattern = null;
        switch (movePattern)
        {
            case eMovePattern.NO_MOVE:
                // —¼•û‚Æ‚ànull‚Ì‚Ü‚Ü
                break;
            case eMovePattern.RANDOM_MOVE:
                normalMovePattern = new MoveRandomPattern(transform);
                findMovePattern = normalMovePattern;
                break;
            case eMovePattern.RANDOM_MOVE_WITH_CHASING:
                normalMovePattern = new MoveRandomPattern(transform);
                findMovePattern = new ChasePattern(transform, "Player");
                break;
            case eMovePattern.RANDOM_MOVE_WITH_EXPLOSION:
                normalMovePattern = new MoveRandomPattern(transform);
                findMovePattern = new ChaseWithExplositonPattern(transform, "Player");
                break;
        }

    }

}
