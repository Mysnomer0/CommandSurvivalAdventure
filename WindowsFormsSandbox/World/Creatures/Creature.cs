﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World
{
    // This class encapsulates all data and functionality pertaining to a creature
    class Creature : GameObject
    {
        // The speed of the creature
        public float speed;
        // The intelligence of the creature.  Higher is better
        public float intelligence;
        // The agility of the creature, used for calculating how well it can dodge attacks and climbing stuff
        public float agility;
        // The weight of the creature, a way to determine the size as well
        public float weight;
        // The overall endurance of the creature.  This value fluctuates based off of things like hunger and how generally tired the creature is
        public float stamana;
        // The overall health of the creature, which is an averaged percentage of the health of all the creatures subparts
        public float health;
        // The muscle content of the creature, which is a sum of the muscle content of each subpart, in weight
        public float muscleContent;
        // The fat content of the creature, which is a sum of the fat content of each subpart, in weight
        public float fatContent;
        // Whether or not the creature is bleeding, anywhere
        public bool isBleeding;
        // Whether or not the creature is sick or unclean, on any part of the body
        public bool isUnclean;

        // The various different stances and positions the creature can take on
        public enum Stances
        {
            STANDING,
            CROUCHING,
            LAYING,
            RUNNING,
            LUNGEING,
            FALLING
        }
        // Converts the stance enum to a string
        public static string StanceToString(Stances stance)
        {
            if (stance == Stances.STANDING)
                return "STANDING";
            else if (stance == Stances.CROUCHING)
                return "CROUCHING";
            else if (stance == Stances.LAYING)
                return "LAYING";
            else if (stance == Stances.RUNNING)
                return "RUNNING";
            else if (stance == Stances.LUNGEING)
                return "LUNGEING";
            else if (stance == Stances.FALLING)
                return "FALLING";
            else
                return "";
        }
        // Converts the string to stance enum 
        public static Stances StringToStance(string stance)
        {
            if (stance == "STANDING")
                return Stances.STANDING;
            else if (stance == "CROUCHING")
                return Stances.CROUCHING;
            else if (stance == "LAYING")
                return Stances.LAYING;
            else if (stance == "RUNNING")
                return Stances.RUNNING;
            else if (stance == "LUNGEING")
                return Stances.LUNGEING;
            else if (stance == "FALLING")
                return Stances.FALLING;
            else
                return Stances.STANDING;
        }
        // Returns whether or not the two stances transition to each other
        public static bool CheckStanceTransition(Stances fromStance, Stances toStance)
        {
            if (fromStance == Stances.STANDING && toStance == Stances.RUNNING
                || fromStance == Stances.STANDING && toStance == Stances.LUNGEING
                || fromStance == Stances.STANDING && toStance == Stances.FALLING
                || fromStance == Stances.STANDING && toStance == Stances.CROUCHING

                || fromStance == Stances.CROUCHING && toStance == Stances.STANDING
                || fromStance == Stances.CROUCHING && toStance == Stances.LAYING
                || fromStance == Stances.CROUCHING && toStance == Stances.LUNGEING

                || fromStance == Stances.LAYING && toStance == Stances.STANDING
                || fromStance == Stances.LAYING && toStance == Stances.CROUCHING

                || fromStance == Stances.RUNNING && toStance == Stances.STANDING
                || fromStance == Stances.RUNNING && toStance == Stances.LUNGEING

                || fromStance == Stances.LUNGEING && toStance == Stances.FALLING
                || fromStance == Stances.LUNGEING && toStance == Stances.CROUCHING

                || fromStance == Stances.FALLING && toStance == Stances.LAYING
                )
                return true;
            return false;
        }

    }
}