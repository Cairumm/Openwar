Public Class MapTile

    Public Property IsCoastal As Boolean
    Public Property IsNuked As Boolean
    Public Property TileType As MapTileType
    Public Property MapRow As Integer
    Public Property MapColumn As Integer

    Public Enum MapTileType
        Empty
        City
        MissileBase
        BomberBase
        ABMBase
        SubmarineBase
    End Enum


End Class
