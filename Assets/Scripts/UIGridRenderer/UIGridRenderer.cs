using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class UIGridRenderer : Graphic
{
    public float thickness=10f;

    public Vector2Int gridSize=new Vector2Int(1,1);

    float width;
    float height;

    float cellWidth;
    float cellHeight;

    public List<Vector2> points;

    public void AddAllPoints()
    {
        for(int y=0;y<gridSize.y;y++)
        {
            for(int x=0;x<gridSize.x;x++)
            {
                AddPoints(x,y);
            }
        }
    }

    public void AddPoints(int x,int y)
    {
        float xPos=cellWidth*x;
        float yPos=cellHeight*y;

        Vector3 newPoint0=new Vector3(xPos,yPos);    //四個點初始
        points.Add(newPoint0);

        Vector3 newPoint1=new Vector3(xPos,yPos+cellHeight);
        points.Add(newPoint1);

        Vector3 newPoint2=new Vector3(xPos+cellWidth,yPos+cellHeight);
        points.Add(newPoint2);

        Vector3 newPoint3=new Vector3(xPos+cellWidth,yPos);
        points.Add(newPoint3);

        //vh.AddTriangle(0,1,2);
        //vh.AddTriangle(2,3,0);

        // float widthSqr=thickness*thickness;
        // float distanceSqr=widthSqr/2f;
        // float distance=Mathf.Sqrt(distanceSqr);

        float distance=thickness;

        Vector3 newPoint4=new Vector3(xPos+distance,yPos+distance);    //四個點初始
        points.Add(newPoint4);

        Vector3 newPoint5=new Vector3(xPos+distance,yPos+(cellHeight-distance));    
        points.Add(newPoint5);

        Vector3 newPoint6=new Vector3(xPos+(cellWidth-distance),yPos+(cellHeight-distance));    
        points.Add(newPoint6);

        Vector3 newPoint7=new Vector3(xPos+(cellWidth-distance),yPos+distance);    
        points.Add(newPoint7);
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        width=rectTransform.rect.width;
        height=rectTransform.rect.height;    //範圍

        

        cellWidth=width/(float)gridSize.x;
        cellHeight=height/(float)gridSize.y;

        int count=0;

        for(int y=0;y<gridSize.y;y++)
        {
            for(int x=0;x<gridSize.x;x++)
            {
                DrawCell(x,y,count,vh);
                count++;
            }
        }
        
        
    }

    //Localize
    private void DrawCell(int x,int y,int index,VertexHelper vh)
    {
        float xPos=cellWidth*x;
        float yPos=cellHeight*y;

        UIVertex vertex=UIVertex.simpleVert;  //初始 畫UI的
        vertex.color=color;



        vertex.position=new Vector3(xPos,yPos);    //四個點初始
        vh.AddVert(vertex);
        

        vertex.position=new Vector3(xPos,yPos+cellHeight);
        vh.AddVert(vertex);
        

        vertex.position=new Vector3(xPos+cellWidth,yPos+cellHeight);
        vh.AddVert(vertex);
        

        vertex.position=new Vector3(xPos+cellWidth,yPos);
        vh.AddVert(vertex);
        

        //vh.AddTriangle(0,1,2);
        //vh.AddTriangle(2,3,0);

        // float widthSqr=thickness*thickness;
        // float distanceSqr=widthSqr/2f;
        // float distance=Mathf.Sqrt(distanceSqr);

        float distance=thickness;

        vertex.position=new Vector3(xPos+distance,yPos+distance);    //四個點初始
        vh.AddVert(vertex);
        

        vertex.position=new Vector3(xPos+distance,yPos+(cellHeight-distance));    
        vh.AddVert(vertex);
        
        vertex.position=new Vector3(xPos+(cellWidth-distance),yPos+(cellHeight-distance));    
        vh.AddVert(vertex);
        
        vertex.position=new Vector3(xPos+(cellWidth-distance),yPos+distance);    
        vh.AddVert(vertex);
        

        int offset=index*8;

        //Top Edge                  
        vh.AddTriangle(offset+1,offset+2,offset+6);    //相框圖形
        vh.AddTriangle(offset+6,offset+5,offset+1);
        //Bottom Edge
        vh.AddTriangle(offset+3,offset+0,offset+4);
        vh.AddTriangle(offset+4,offset+7,offset+3);
        //Left Edge
        vh.AddTriangle(offset+0,offset+1,offset+5);
        vh.AddTriangle(offset+5,offset+4,offset+0);
        //Right Edge
        vh.AddTriangle(offset+2,offset+3,offset+7);
        vh.AddTriangle(offset+7,offset+6,offset+2);
    }



    
}
