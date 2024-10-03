using System;
using Unity.Collections;
using Unity.Netcode;

public struct PlayerData : IEquatable<PlayerData>, INetworkSerializable
{
    public ulong clientId;
    public int colorId;
    public FixedString64Bytes playerName;
    public FixedString64Bytes playerId;

    public bool Equals(PlayerData other)
    {
        return clientId == other.clientId
            && colorId == other.colorId
            && playerName.Equals(other.playerName)
            && playerId.Equals(other.playerId);
    }

    public override bool Equals(object obj)
    {
        return obj is PlayerData other && Equals(other);
    }

    public override int GetHashCode()
    {
        return clientId.GetHashCode();
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref clientId);
        serializer.SerializeValue(ref colorId);
        serializer.SerializeValue(ref playerName);
        serializer.SerializeValue(ref playerId);
    }
}