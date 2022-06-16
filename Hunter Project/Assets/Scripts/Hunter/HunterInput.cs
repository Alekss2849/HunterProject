using UnityEngine;

namespace HunterProject.Hunter
{
    public class HunterInput
    {
        public MovementState TrackMovement()
        {
            if (Input.GetKey(KeyCode.W))
            {
                return MovementState.MoveUp;
            }

            if (Input.GetKey(KeyCode.S))
            {
                return MovementState.MoveDown;
            }

            if (Input.GetKey(KeyCode.D))
            {
                return MovementState.MoveRight;
            }

            if (Input.GetKey(KeyCode.A))
            {
                return MovementState.MoveLeft;
            }

            return MovementState.Idle;
        }

        public bool TrackShoot()
        {
            return Input.GetKeyUp(KeyCode.Mouse0);
        }
    }
}