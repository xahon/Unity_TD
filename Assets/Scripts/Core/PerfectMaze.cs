using System;
using System.Collections.Generic;
using System.Text;
using Extensions;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Core {

    // Source: https://habr.com/post/262345/
    public class PerfectMaze {

        public enum CellType {
            None,
            Road,
            WrongRoad,
            EnemyBase,
            PlayerBase,
        }

        private class NeighbourhoodInfo {
            public Vector2Int Position;
            public CellType   Type;
        }

        public uint                             Size         { get; }
        public Dictionary<Vector2Int, CellType> Map          { get; }
        public List<Vector2Int>                 VisitedCells { get; private set; }

        public PerfectMaze(uint size) {
            Size = size;
            Map  = new Dictionary<Vector2Int, CellType>((int) (Size * Size));

            Generate();
        }

        private void Generate() {
            List<Vector2Int> unvisited = new List<Vector2Int>((int) Size);

            for (int x = 0; x < Size; x++) {
                for (int y = 0; y < Size; y++) {
                    Vector2Int pos = new Vector2Int(x, y);

                    Map[pos] = CellType.None;

                    if (x % 2 != 0 && y % 2 != 0) {
                        unvisited.Add(pos);
                    }
                }
            }

            if (unvisited.Count < 9) {
                throw new Exception("Field is too small");
            }


            Vector2Int enemyBase  = default(Vector2Int),
                       playerBase = default(Vector2Int);

            unvisited.Shuffle();
            bool solutionFound = false;

            // Make player and enemy base to be spaced minimum by one empty cell
            for (int eI = 0; eI < unvisited.Count; eI++) {
                enemyBase = unvisited[eI];

                for (int pI = 0; pI < unvisited.Count; pI++) {
                    if (eI == pI) {
                        continue;
                    }

                    playerBase = unvisited[pI];

                    // If it is not an empty cell with two spaces around skip them as too tight
                    if (Math.Abs(playerBase.x - enemyBase.x) < 2 || Math.Abs(playerBase.y - enemyBase.y) < 2) continue;

                    solutionFound = true;
                    break;
                }

                if (solutionFound) {
                    break;
                }
            }

            Debug.Assert(enemyBase != null, nameof(enemyBase) + " != null");
            Debug.Assert(playerBase != null, nameof(playerBase) + " != null");

            VisitedCells = new List<Vector2Int>(unvisited.Count) {enemyBase};

            Map[enemyBase]  = CellType.EnemyBase;
            Map[playerBase] = CellType.PlayerBase;

            int ii = 0;
            while (VisitedCells.Count > 0) {
                if (++ii >= 2000) {
                    UnityEngine.Debug.LogError("Overflow");
                    break;
                }

                Vector2Int        currentPos          = VisitedCells[VisitedCells.Count - 1];
                NeighbourhoodInfo neighbour           = FindAroundAll(currentPos, CellType.None).RandomElementOrDefault(null);
                NeighbourhoodInfo playerBaseNeighbour = FindAroundAll(currentPos, CellType.PlayerBase).RandomElementOrDefault(null);

                if (playerBaseNeighbour != null) {
                    Map[currentPos.MiddlePosition(playerBaseNeighbour.Position)] = CellType.Road;
                    VisitedCells.Add(playerBaseNeighbour.Position);
                    break;
                }

                if (neighbour == null) {
                    Vector2Int prevPos = VisitedCells[VisitedCells.Count - 1];
                    VisitedCells.RemoveAt(VisitedCells.Count - 1);

                    Map[prevPos.MiddlePosition(currentPos)] = CellType.WrongRoad;
                    Map[prevPos]                            = CellType.WrongRoad;

                    continue;
                }

                Map[neighbour.Position] = CellType.Road;
                VisitedCells.Add(neighbour.Position);
            }

            for (int i = 1; i < VisitedCells.Count; i++) {
                Map[VisitedCells[i - 1].MiddlePosition(VisitedCells[i])] = CellType.Road;
            }

            StringBuilder sb   = new StringBuilder((int) (Size * Size));
            Vector2Int    pos1 = new Vector2Int(0, 0);

            for (int y = 0; y < Size; y++) {
                for (int x = 0; x < Size; x++) {
                    pos1.Set(x, y);
                    sb.Append((int) Map[pos1]);
                    sb.Append(" ");
                }

                sb.Append("\n");
            }

            UnityEngine.Debug.Log(sb.ToString());
        }

        private bool IsPositionWithinField(Vector2Int position) {
            return position.x >= 0 && position.x < Size && position.y >= 0 && position.y < Size;
        }

        private List<NeighbourhoodInfo> FindAroundAll(Vector2Int cellPosition, CellType type) {
            List<NeighbourhoodInfo> neighbours = new List<NeighbourhoodInfo>(4);

            {
                var position = new Vector2Int(cellPosition.x - 2, cellPosition.y);
                if (IsPositionWithinField(position)) {
                    neighbours.Add(new NeighbourhoodInfo {
                        Type     = Map[position],
                        Position = position
                    });
                }
            }

            {
                var position = new Vector2Int(cellPosition.x + 2, cellPosition.y);
                if (IsPositionWithinField(position)) {
                    neighbours.Add(new NeighbourhoodInfo {
                        Type     = Map[position],
                        Position = position
                    });
                }
            }

            {
                var position = new Vector2Int(cellPosition.x, cellPosition.y - 2);
                if (IsPositionWithinField(position)) {
                    neighbours.Add(new NeighbourhoodInfo {
                        Type     = Map[position],
                        Position = position
                    });
                }
            }

            {
                var position = new Vector2Int(cellPosition.x, cellPosition.y + 2);
                if (IsPositionWithinField(position)) {
                    neighbours.Add(new NeighbourhoodInfo {
                        Type     = Map[position],
                        Position = position
                    });
                }
            }

            if (neighbours.Count == 0) {
                return null;
            }

            List<NeighbourhoodInfo> neighboursWithType = new List<NeighbourhoodInfo>(4);

            foreach (NeighbourhoodInfo neighbour in neighbours) {
                if (neighbour.Type == type) {
                    neighboursWithType.Add(neighbour);
                }
            }

            return neighboursWithType;
        }
    }

}