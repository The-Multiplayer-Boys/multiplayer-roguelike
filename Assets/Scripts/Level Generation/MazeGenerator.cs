using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum WALL_DIRECTIONS: int {
    NORTH = 0b1000,
    SOUTH = 0b0100,
    EAST = 0b0010,
    WEST = 0b0001
}

public class Cell {
    public int x;
    public int y;
    public Cell north;
    public Cell south;
    public Cell east;
    public Cell west;
    public bool visited;
    public int walls;
    
    public Cell(int x, int y) {
        this.x = x;
        this.y = y;
        this.north = null;
        this.south = null;
        this.east = null;
        this.west = null;
        this.visited = false;
        this.walls = 0b1111;
    }
    
    public List<Cell> neighbors() {
        Cell[] dirs = new Cell[]{this.north, this.south, this.east, this.west};

        return (
            from i in dirs
            where i != null
            select i
            ).ToList();
    }
}

public class Board {
    public int width;
    public int height;
    public List<List<Cell>> board;
    
    public Board(int width, int height) {
        this.width = width;
        this.height = height;

        var board = (from y in Enumerable.Range(0, height)
            select (from x in Enumerable.Range(0, width)
                select new Cell(x, y)).ToList()).ToList();

        foreach (var y in Enumerable.Range(0, height)) {
            foreach (var x in Enumerable.Range(0, width)) {
                if (Enumerable.Range(0, height).Contains(y - 1)) {
                    board[y][x].north = board[y - 1][x];
                }
                if (Enumerable.Range(0, height).Contains(y + 1)) {
                    board[y][x].south = board[y + 1][x];
                }
                if (Enumerable.Range(0, width).Contains(x + 1)) {
                    board[y][x].east = board[y][x + 1];
                }
                if (Enumerable.Range(0, width).Contains(x - 1)) {
                    board[y][x].west = board[y][x - 1];
                }
            }
        }
        this.board = board;
    }
    
    // public void show() {
    //     var nodes = "╋┣┫┃┻┗┛╹┳┏┓╻━╺╸ ";
    //     foreach (var y in Enumerable.Range(0, this.height)) {
    //         foreach (var x in Enumerable.Range(0, this.width)) {
    //             Debug.Log(nodes[this.board[y][x].walls]);
    //         }
    //     }
    // }
}


public class MazeGenerator : MonoBehaviour
{
    public int width;
    public int height;

    // TODO: find out how to set current cell.visited to true
    void dfs(int current_x, int current_y, Board board) {
        var current_cell = board.board[current_y][current_x];
        current_cell.visited = true;

        while (true) {
            var unvisited = (
                from i in current_cell.neighbors()
                where !i.visited
                select i
                ).ToList();
            if (unvisited.Count == 0) {
                break;
            }
            int rand = UnityEngine.Random.Range(0, unvisited.Count);
            
            var next_cell = unvisited[rand];

            Cell north = (Cell) current_cell.north;
            if(next_cell == current_cell.north){
                current_cell.walls -= (int)WALL_DIRECTIONS.NORTH;
                next_cell.walls -= (int)WALL_DIRECTIONS.SOUTH;
            } else if(next_cell == current_cell.south){
                current_cell.walls -= (int)WALL_DIRECTIONS.SOUTH;
                next_cell.walls -= (int)WALL_DIRECTIONS.NORTH;
            } else if(next_cell == current_cell.east){
                current_cell.walls -= (int)WALL_DIRECTIONS.EAST;
                next_cell.walls -= (int)WALL_DIRECTIONS.WEST;
            } else if(next_cell == current_cell.west){
                current_cell.walls -= (int)WALL_DIRECTIONS.WEST;
                next_cell.walls -= (int)WALL_DIRECTIONS.EAST;
            }

            dfs(next_cell.x, next_cell.y, board);
        }
        return;
    }

    void Start()
    {
        int x = 0;
        int y = 0;
        Board b = new Board(width, height);
        dfs(x, y, b);
        // Debug.Log("doneso");
        // b.show();
    }

    void Update()
    {
        
    }
}
